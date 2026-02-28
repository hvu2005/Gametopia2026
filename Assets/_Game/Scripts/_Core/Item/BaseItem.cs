using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour, IEquippable
{
    public string ItemID { get; protected set; }
    public string ItemName { get; protected set; }
    public int Tier { get; protected set; } // Sao của vật phẩm (1-3)
    public ItemType Type { get; protected set; }
    public SynergyType SetBonus { get; protected set; }

    public EquipmentSlotType AllowedSlot {get; protected set;}

    public Stats Stats;
    
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