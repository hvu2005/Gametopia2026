using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Game/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [SerializeField] private string itemId;
    [SerializeField] private string itemName;

    [SerializeField] private Sprite sprite;

    [SerializeField] private ItemType type;
    [SerializeField] private RarityType rarity;
    [SerializeField] private EquipmentSlotType slotType;

    [SerializeField] private Stats stats;

    // Getters
    public string ItemId => itemId;
    public string ItemName => itemName;
    public Sprite Sprite => sprite;
    public ItemType Type => type;
    public RarityType Rarity => rarity;
    public EquipmentSlotType SlotType => slotType;
    public Stats Stats => stats;
}