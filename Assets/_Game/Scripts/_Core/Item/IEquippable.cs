public interface IEquippable
{
    EquipmentSlotType AllowedSlot { get; }
    void OnEquip(BaseEntity entity);
    void OnUnequip(BaseEntity entity);
}