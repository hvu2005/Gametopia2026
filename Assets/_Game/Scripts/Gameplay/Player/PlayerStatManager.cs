using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Quản lý chỉ số tổng hợp của Player theo Modifier Pipeline.
///
/// Pipeline thứ tự xử lý:
///   1. BaseStats (cấu hình trực tiếp trên Inspector)
///   2. Flat Modifier  → cộng thẳng
///   3. PercentAdd     → % cộng dồn, nhân 1 lần vào (base + flat)
///   4. PercentMult    → nhân lần lượt vào tổng sau PercentAdd
///
/// Cách dùng:
///   - Effect gọi AddModifier(mod) khi equip
///   - Effect gọi RemoveModifiers(sourceID) khi unequip
///   - Mọi thay đổi đều tự trigger Recalculate() và emit OnStatsChanged
/// </summary>
public class PlayerStatManager : MonoBehaviour
{
    [Header("Base Stats (Inspector)")]
    [Tooltip("Chỉ số gốc của Player — không thay đổi bởi item")]
    [SerializeField] private Stats baseStats;

    public Stats FinalStats { get; private set; }
    public Stats BaseStats => baseStats;

    private readonly List<StatModifier> _modifiers = new();

    // ─── Modifier Management ─────────────────────────────────────────────────

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);
        Recalculate();
    }

    /// <summary>Xóa toàn bộ modifier có SourceID trùng với sourceID cho trước.</summary>
    public void RemoveModifiers(string sourceID)
    {
        int removed = _modifiers.RemoveAll(m => m.SourceID == sourceID);
        if (removed > 0) Recalculate();
    }

    public void ClearAllModifiers()
    {
        _modifiers.Clear();
        Recalculate();
    }

    // ─── Pipeline Recalculate ────────────────────────────────────────────────

    public void Recalculate()
    {
        Dictionary<StatType, float> result = baseStats.ToDictionary();

        // ── Pass 1: Flat ─────────────────────────────────────────────────────
        foreach (var mod in _modifiers.Where(m => m.ModifierType == ModifierType.Flat))
        {
            result[mod.StatType] += mod.Value;
        }

        // ── Pass 2: PercentAdd ───────────────────────────────────────────────
        // Gom lại tất cả PercentAdd theo StatType, cộng dồn rồi nhân 1 lần
        var percentAddSums = new Dictionary<StatType, float>();
        foreach (var mod in _modifiers.Where(m => m.ModifierType == ModifierType.PercentAdd))
        {
            percentAddSums.TryGetValue(mod.StatType, out float existing);
            percentAddSums[mod.StatType] = existing + mod.Value;
        }
        foreach (var kv in percentAddSums)
        {
            result[kv.Key] *= (1f + kv.Value);
        }

        // ── Pass 3: PercentMult ──────────────────────────────────────────────
        // Nhân lần lượt từng modifier
        foreach (var mod in _modifiers.Where(m => m.ModifierType == ModifierType.PercentMult))
        {
            result[mod.StatType] *= mod.Value;
        }

        FinalStats = Stats.FromDictionary(result);
        EventBus.Emit(ItemEventType.OnStatsChanged, FinalStats);
    }

    // ─── Legacy IStatProvider Support ────────────────────────────────────────
    // Giữ lại để không break code cũ đang dùng Register/Unregister

    private readonly List<IStatProvider> _legacyProviders = new();

    /// <obsolete>Dùng AddModifier/RemoveModifiers thay thế.</obsolete>
    public void Register(IStatProvider provider)
    {
        if (!_legacyProviders.Contains(provider))
        {
            _legacyProviders.Add(provider);
            // Convert legacy stats sang Flat modifiers
            ApplyLegacyProvider(provider);
        }
    }

    /// <obsolete>Dùng AddModifier/RemoveModifiers thay thế.</obsolete>
    public void Unregister(IStatProvider provider)
    {
        if (_legacyProviders.Remove(provider))
        {
            RemoveModifiers("legacy_" + provider.GetHashCode());
        }
    }

    private void ApplyLegacyProvider(IStatProvider provider)
    {
        string sourceID = "legacy_" + provider.GetHashCode();
        Stats bonus = provider.GetBonusStats();
        Dictionary<StatType, float> bonusDict = bonus.ToDictionary();

        foreach (var kv in bonusDict)
        {
            if (kv.Value != 0f)
                AddModifier(new StatModifier(kv.Key, kv.Value, ModifierType.Flat, sourceID));
        }
    }

    // ─── Unity Lifecycle ─────────────────────────────────────────────────────

    private void Awake()
    {
        FinalStats = baseStats;
    }
}
