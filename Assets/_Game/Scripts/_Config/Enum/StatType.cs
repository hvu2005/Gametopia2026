/// <summary>
/// Enum định danh từng loại chỉ số trong game.
/// Dùng làm key trong Modifier Pipeline và Dictionary khi tính toán.
/// Thêm stat mới chỉ cần thêm entry vào đây, không cần sửa code tính toán.
/// </summary>
public enum StatType
{
    HP,
    PhysicalDamage,
    MagicDamage,
    Defense,
    CriticalChance,
    CriticalDamage,
    LifeSteal,
    Fortune,
    Poisonous,
    StunChance
}
