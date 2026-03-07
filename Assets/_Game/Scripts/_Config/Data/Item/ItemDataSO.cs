using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Game/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [Header("Identity")]
    public string ItemID;
    public string ItemName;
    [TextArea(2, 4)]
    public string Description;

    [Header("Visual")]
    public Sprite Icon;

    [Header("Classification")]
    public ItemType Type;
    public RarityType Rarity;
    public EquipmentSlotType AllowedSlot;

    [Header("Stat Buffs")]
    [Tooltip("Chỉ số sẽ cộng vào player khi equip item này")]
    public Stats BuffStats;
}
