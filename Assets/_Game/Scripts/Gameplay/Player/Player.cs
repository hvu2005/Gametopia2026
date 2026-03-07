using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
    public BaseEquipmentFrame EquipmentFrame { get; protected set; }

    [Header("Item System")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerStatManager statManager;

    public PlayerInventory Inventory => inventory;
    public PlayerStatManager StatManager => statManager;

    public BaseEnemy[] dummies;

    void Start()
    {
        if (statManager != null && inventory != null)
        {
            statManager.Register(inventory);
        }

        // Sync FinalStats -> BaseEntity.Stats để BattleSystem dùng đúng chỉ số khi equip item
        EventBus.On<Stats>(ItemEventType.OnStatsChanged, OnStatsChanged);
    }

    void OnDestroy()
    {
        EventBus.Off<Stats>(ItemEventType.OnStatsChanged, OnStatsChanged);
    }

    private void OnStatsChanged(Stats finalStats)
    {
        this.Stats = finalStats;
    }


}