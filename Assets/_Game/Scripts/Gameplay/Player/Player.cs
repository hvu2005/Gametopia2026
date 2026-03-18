using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
    public HiddenStats hiddenStats; 

    public BaseEquipmentFrame EquipmentFrame { get; protected set; }

    [Header("Item System")]
    [SerializeField] private PlayerStatManager statManager;

    public PlayerStatManager StatManager => statManager;

    public override void Start()
    {
        base.Start();

        // Sync FinalStats -> BaseEntity.Stats để BattleSystem dùng đúng chỉ số khi equip item
        // EventBus.On<Stats>(ItemEventType.OnStatsChanged, OnStatsChanged);
        EventBus.Emit<Stats>(PlayerEventType.UpdateStats, this.Stats);

    }

    void OnDestroy()
    {
        // EventBus.Off<Stats>(ItemEventType.OnStatsChanged, OnStatsChanged);
    }


    public override void OnUpdateStat()
    {
        base.OnUpdateStat();

        EventBus.Emit<Stats>(PlayerEventType.UpdateStats, this.Stats);
    }

}