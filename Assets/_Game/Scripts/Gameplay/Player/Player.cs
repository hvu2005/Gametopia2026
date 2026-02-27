public abstract class Player : BaseEntity
{
    // Chứa bộ khung trang bị của riêng Player (Issue 3)
    public BaseEquipmentFrame EquipmentFrame { get; protected set; }
}