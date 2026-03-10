using UnityEngine;

public class Player : BaseEntity
{
    [Header("Item System")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private EquipmentManager equipmentManager;

    public PlayerInventory Inventory => inventory;
    public PlayerStatManager StatManager => statManager;
    public EquipmentManager Equipment => equipmentManager;

    void Start()
    {
        // Stats giờ flow qua EquipmentManager -> Effects -> PlayerStatManager.AddModifier
        // Không cần Register inventory trực tiếp nữa
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