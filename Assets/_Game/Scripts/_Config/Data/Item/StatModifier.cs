using System;

/// <summary>
/// Đại diện cho một giá trị thay đổi lên một StatType cụ thể.
/// Dùng trong Modifier Pipeline của PlayerStatManager.
/// Source dùng để remove đúng nhóm modifier khi unequip.
/// </summary>
[Serializable]
public class StatModifier
{
    public StatType StatType;
    public float Value;
    public ModifierType ModifierType;

    /// <summary>
    /// Nguồn gốc của modifier — thường là ItemInstance.Guid hoặc tên Trait milestone.
    /// Dùng để remove toàn bộ modifier của một source cụ thể.
    /// </summary>
    [NonSerialized]
    public string SourceID;

    public StatModifier(StatType statType, float value, ModifierType modifierType, string sourceID = "")
    {
        StatType = statType;
        Value = value;
        ModifierType = modifierType;
        SourceID = sourceID;
    }

    /// <summary>Constructor cho Unity Inspector (field công khai, không qua constructor)</summary>
    public StatModifier() { }
}
