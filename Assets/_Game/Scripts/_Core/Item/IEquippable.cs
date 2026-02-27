public interface IEquippable
{
    EquipmentSlot AllowedSlot { get; }
    void OnEquip(BaseEntity entity);
    void OnUnequip(BaseEntity entity);
}