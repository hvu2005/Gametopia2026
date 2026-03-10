using UnityEngine;

/// <summary>
/// ⚠️ DEPRECATED: Dùng <see cref="ItemDefinitionSO"/> thay thế.
/// Class này chỉ giữ lại để Unity có thể load các .asset cũ đã tạo trước khi migrate.
/// Không tạo item mới bằng class này.
/// </summary>
[System.Obsolete("Dùng ItemDefinitionSO thay thế. Class này chỉ còn để tương thích với các .asset cũ.")]
[CreateAssetMenu(fileName = "Item_DEPRECATED_", menuName = "Game/[DEPRECATED] Item Data (old)")]
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

    [Header("Stat Buffs — DEPRECATED (dùng Effects[] trong ItemDefinitionSO)")]
    [Tooltip("Chỉ số sẽ cộng vào player khi equip item này")]
    public Stats BuffStats;
}
