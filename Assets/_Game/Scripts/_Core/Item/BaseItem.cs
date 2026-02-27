using System.Collections.Generic;

public abstract class BaseItem : IEquippable
{
    public string ItemID { get; protected set; }
    public string ItemName { get; protected set; }
    public int Level { get; protected set; } // Sao của vật phẩm (1-3)
    public ItemType Type { get; protected set; }
    public SynergyType SetBonus { get; protected set; }

    public EquipmentSlot AllowedSlot {get; protected set;}

    public abstract List<StatModifier> GetItemStats();
    
    // Hàm xử lý khi người chơi kéo vật phẩm này đè lên vật phẩm khác để nâng cấp
    public abstract bool TryMergeWith(BaseItem otherItem);
    
    // Triển khai logic khi mặc/tháo đồ (VD: Cộng/trừ chỉ số vào nhân vật)
    public virtual void OnEquip(BaseEntity character) {
        // Logic cộng stat cơ bản
    }
    
    public virtual void OnUnequip(BaseEntity character) {
        // Logic trừ stat cơ bản
    }
}