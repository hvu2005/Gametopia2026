using System;

/// <summary>
/// Đại diện cho MỘT item cụ thể trong game (Flyweight pattern).
/// Chỉ lưu dữ liệu THAY ĐỔI được — mọi dữ liệu tĩnh (Name, Icon, Effects)
/// đều đọc từ Definition để tiết kiệm bộ nhớ.
///
/// SAVE/LOAD NOTE: Khi serialize để save game, lưu lại Guid, DefinitionID,
/// StarLevel và Rarity — KHÔNG tạo Guid mới khi load. Dùng ItemInstance(string guid, ...)
/// để restore đúng instance.
/// </summary>
[Serializable]
public class ItemInstance
{
    // ─── Identity ────────────────────────────────────────────────────────────

    /// <summary>ID duy nhất của instance này — dùng làm SourceID cho StatModifier.</summary>
    public string Guid { get; private set; }

    public ItemDefinitionSO Definition { get; private set; }
    public int StarLevel { get; private set; }         // 0–3
    public RarityType Rarity { get; private set; }

    // ─── Constructors ────────────────────────────────────────────────────────

    /// <summary>Tạo instance mới với Guid tự sinh (dùng khi Spawn/Drop).</summary>
    public ItemInstance(ItemDefinitionSO definition, int starLevel = 0, RarityType rarity = RarityType.Normal)
    {
        Guid = System.Guid.NewGuid().ToString();
        Definition = definition;
        StarLevel = starLevel;
        Rarity = rarity;
    }

    /// <summary>Tạo instance từ Guid đã lưu (dùng khi Load game).</summary>
    public ItemInstance(string existingGuid, ItemDefinitionSO definition, int starLevel, RarityType rarity)
    {
        Guid = existingGuid;
        Definition = definition;
        StarLevel = starLevel;
        Rarity = rarity;
    }

    // ─── Stat Computation ────────────────────────────────────────────────────

    /// <summary>
    /// Tính Stats thực tế của instance này sau khi áp dụng Star và Rarity scaling.
    /// Công thức: (BaseStats + StatStepPerStar * StarLevel) * RarityMultiplier
    /// </summary>
    public Stats ComputeScaledStats()
    {
        Stats scaled = Definition.BaseStats + (Definition.StatStepPerStar * StarLevel);
        float mult = Definition.GetRarityMultiplier(Rarity);
        return scaled * mult;
    }

    // ─── Convenience ─────────────────────────────────────────────────────────

    public string DisplayName => Definition != null ? Definition.ItemName : "Unknown";
    public EquipmentSlotType AllowedSlot => Definition.AllowedSlot;
    public SynergyType[] Traits => Definition.DefaultTraits;
}
