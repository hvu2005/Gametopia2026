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

void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        List<BaseEnemy> activeEnemies = new List<BaseEnemy>();

        foreach (var enemy in dummies)
        {
            if (enemy.gameObject.activeSelf)
            {
                activeEnemies.Add(enemy);
            }
        }

        if (activeEnemies.Count > 0)
        {
            int randomIndex = Random.Range(0, activeEnemies.Count);
            BattleSystem.Instance.StartBattle(activeEnemies[randomIndex]);
        }
    }
}
}