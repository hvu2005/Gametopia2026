using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
    public BaseEquipmentFrame EquipmentFrame { get; protected set; }

    [Header("Item System")]
    [SerializeField] private PlayerStatManager statManager;

    public PlayerStatManager StatManager => statManager;

    void Start()
    {


        // Sync FinalStats -> BaseEntity.Stats để BattleSystem dùng đúng chỉ số khi equip item
        // EventBus.On<Stats>(ItemEventType.OnStatsChanged, OnStatsChanged);
    }

    void OnDestroy()
    {
        // EventBus.Off<Stats>(ItemEventType.OnStatsChanged, OnStatsChanged);
    }




}