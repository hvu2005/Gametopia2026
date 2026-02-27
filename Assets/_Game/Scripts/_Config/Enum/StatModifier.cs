public enum ModifierType {
    Flat,       // Cộng thẳng (VD: +10 Atk)
    Percentage  // Cộng theo phần trăm (VD: +15% Atk)
}

// Cấu trúc lưu trữ một thay đổi chỉ số từ đồ hoặc kỹ năng
public struct StatModifier {
    public StatType Type;
    public float Value;
    public ModifierType ModType;
    public object Source; // Nguồn buff (Item, Skill...) để dễ gỡ bỏ sau này
}