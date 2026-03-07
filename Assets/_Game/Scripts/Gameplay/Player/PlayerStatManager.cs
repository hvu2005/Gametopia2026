using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [Header("Base Stats (Inspector)")]
    [Tooltip("Chỉ số gốc của player, cấu hình trong Inspector")]
    [SerializeField] private Stats baseStats;

    public Stats FinalStats { get; private set; }
    public Stats BaseStats => baseStats;

    private readonly List<IStatProvider> _providers = new();

    public void Register(IStatProvider provider)
    {
        if (!_providers.Contains(provider))
        {
            _providers.Add(provider);
            Recalculate();
        }
    }

    public void Unregister(IStatProvider provider)
    {
        if (_providers.Remove(provider))
        {
            Recalculate();
        }
    }

    public void Recalculate()
    {
        Stats totalBonus = new Stats();
        foreach (var provider in _providers)
        {
            totalBonus = totalBonus + provider.GetBonusStats();
        }

        FinalStats = baseStats + totalBonus;

        EventBus.Emit(ItemEventType.OnStatsChanged, FinalStats);
    }

    public Stats GetTotalBonus()
    {
        Stats totalBonus = new();
        foreach (var provider in _providers)
        {
            totalBonus = totalBonus + provider.GetBonusStats();
        }
        return totalBonus;
    }

    private void Awake()
    {
        FinalStats = baseStats;
    }
}
