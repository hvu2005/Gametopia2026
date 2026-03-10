using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dữ liệu TĨNH định nghĩa một loại item.
/// Đây là Flyweight: nhiều ItemInstance cùng trỏ về 1 ItemDefinitionSO.
/// Effects là các SO độc lập (Shareable) — kéo vào nhiều Item để tái sử dụng.
/// </summary>
[CreateAssetMenu(fileName = "ItemDef_", menuName = "Game/Item Definition")]
public class ItemDefinitionSO : ScriptableObject
{
    // ─── Identity ────────────────────────────────────────────────────────────
    [Header("Identity")]
    [UnityEngine.Serialization.FormerlySerializedAs("ItemID")]
    public string ItemID;

    [UnityEngine.Serialization.FormerlySerializedAs("ItemName")]
    public string ItemName;

    [TextArea(2, 4)]
    [UnityEngine.Serialization.FormerlySerializedAs("Description")]
    public string Description;

    // ─── Visual ──────────────────────────────────────────────────────────────
    [Header("Visual")]
    [UnityEngine.Serialization.FormerlySerializedAs("Icon")]
    public Sprite Icon;

    // ─── Classification ──────────────────────────────────────────────────────
    [Header("Classification")]
    [UnityEngine.Serialization.FormerlySerializedAs("Type")]
    public ItemType Type;

    [UnityEngine.Serialization.FormerlySerializedAs("AllowedSlot")]
    public EquipmentSlotType AllowedSlot;

    /// <summary>Các Tộc/Hệ mà item này thuộc về. Hỗ trợ nhiều Trait cùng lúc.</summary>
    public SynergyType[] DefaultTraits = new SynergyType[0];

    // ─── Base Stats & Star Scaling ───────────────────────────────────────────
    [Header("Base Stats & Star Scaling")]
    [Tooltip("Chỉ số gốc ở Star 0. Nếu item không có cơ chế Star, để StatStepPerStar = 0.")]
    public Stats BaseStats;

    [Tooltip("Chỉ số cộng thêm mỗi cấp Star. Để 0 nếu item không scale theo Star.")]
    public Stats StatStepPerStar;

    // ─── Rarity Config ───────────────────────────────────────────────────────
    [Header("Rarity Config")]
    [Tooltip("Multiplier nhân vào BaseStats theo Rarity. Không bắt buộc — nếu rỗng, multiplier = 1.0")]
    public RarityConfig[] RarityConfigs = new RarityConfig[0];

    // ─── Effects ─────────────────────────────────────────────────────────────
    [Header("Effects")]
    [Tooltip("Các SO Effect độc lập. Có thể share 1 Effect SO giữa nhiều Item để re-balance nhanh.")]
    public BaseItemEffect[] Effects = new BaseItemEffect[0];

    // ─── Helpers ─────────────────────────────────────────────────────────────

    /// <summary>Lấy RarityMultiplier cho Rarity cho trước. Trả 1.0 nếu không cấu hình.</summary>
    public float GetRarityMultiplier(RarityType rarity)
    {
        foreach (var cfg in RarityConfigs)
            if (cfg.Rarity == rarity) return cfg.StatMultiplier;
        return 1f;
    }
}

/// <summary>Cấu hình nhân chỉ số theo Rarity.</summary>
[System.Serializable]
public class RarityConfig
{
    public RarityType Rarity;
    [Tooltip("1.0 = không thay đổi. 1.5 = +50% tất cả stat.")]
    public float StatMultiplier = 1f;
}
