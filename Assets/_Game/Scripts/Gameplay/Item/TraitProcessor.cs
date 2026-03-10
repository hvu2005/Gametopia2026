using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tính toán và theo dõi Synergy Trait đang hoạt động.
/// Lắng nghe OnInventoryChanged → đếm lại Trait → kích hoạt/tắt mốc.
///
/// Đặt component này trên cùng GameObject với Player.
/// Yêu cầu: EquipmentManager để duyệt item đang mặc.
/// </summary>
public class TraitProcessor : MonoBehaviour
{
    [Header("Trait Definitions")]
    [Tooltip("Danh sách tất cả TraitDefinitionSO trong game. Kéo vào đây.")]
    [SerializeField] private TraitDefinitionSO[] allTraits;

    [Header("References")]
    [SerializeField] private Player player;

    private readonly Dictionary<SynergyType, int> _traitCounts = new();

    // Lưu (effect, sourceID) thay vì (effect, ItemInstance)
    // sourceID có dạng "trait_MachineName_2" — ổn định để Remove chính xác
    private readonly List<(BaseItemEffect effect, string sourceID)> _activeMilestoneEffects = new();

    // ─── Unity Lifecycle ─────────────────────────────────────────────────────

    private void Awake()
    {
        EventBus.On<int>(ItemEventType.OnInventoryChanged, OnInventoryChanged);
    }

    private void OnDestroy()
    {
        EventBus.Off<int>(ItemEventType.OnInventoryChanged, OnInventoryChanged);
    }

    // ─── Core Logic ──────────────────────────────────────────────────────────

    private void OnInventoryChanged(int _) => RecalculateTraits();

    public void RecalculateTraits()
    {
        // 1. Deactivate tất cả milestone đang bật (truyền null item — StatEffect đã handle)
        foreach (var (effect, _) in _activeMilestoneEffects)
            effect.OnUnequip(null, player);
        _activeMilestoneEffects.Clear();

        // 2. Đếm lại số lượng Trait từ EquipmentManager
        _traitCounts.Clear();
        if (player == null || player.Equipment == null) return;

        foreach (var instance in player.Equipment.GetAllEquipped())
        {
            if (instance?.Definition == null) continue;
            foreach (var trait in instance.Traits)
            {
                _traitCounts.TryGetValue(trait, out int count);
                _traitCounts[trait] = count + 1;
            }
        }

        // 3. Kích hoạt mốc tương ứng — dùng sourceID ổn định
        foreach (var traitDef in allTraits)
        {
            if (!_traitCounts.TryGetValue(traitDef.TraitType, out int count)) continue;

            foreach (var milestone in traitDef.Milestones)
            {
                if (count >= milestone.RequiredCount && milestone.MilestoneEffect != null)
                {
                    // Source ID dạng: "trait_Machine_2" — nhất quán giữa equip/unequip
                    string sourceID = $"trait_{traitDef.TraitType}_{milestone.RequiredCount}";
                    milestone.MilestoneEffect.OnEquip(null, player);
                    _activeMilestoneEffects.Add((milestone.MilestoneEffect, sourceID));
                }
            }
        }

        // 4. Broadcast để Synergy UI cập nhật
        EventBus.Emit(ItemEventType.OnSynergyChanged, _traitCounts);
    }

    // ─── Debug ───────────────────────────────────────────────────────────────

    public IReadOnlyDictionary<SynergyType, int> TraitCounts => _traitCounts;
}
