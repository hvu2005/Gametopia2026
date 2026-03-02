using UnityEngine;

public class Player : BaseEntity
{
    public BaseEquipmentFrame EquipmentFrame { get; protected set; }

    [Header("Item System")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerStatManager statManager;

    public PlayerInventory Inventory => inventory;
    public PlayerStatManager StatManager => statManager;

    public BaseEnemy dummy;

    void Start()
    {
        if (statManager != null && inventory != null)
        {
            statManager.Register(inventory);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BattleSystem.Instance.StartBattle(dummy);
        }
    }
}