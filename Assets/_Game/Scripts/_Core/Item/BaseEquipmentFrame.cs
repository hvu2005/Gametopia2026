using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEquipmentFrame : MonoBehaviour {
    // Lưu trữ vật phẩm theo vị trí tương ứng (Weapon -> Item, Head -> Item...)
    protected Dictionary<EquipmentSlotType, BaseItem> EquippedItems;

    public BaseEquipmentFrame() {
        EquippedItems = new Dictionary<EquipmentSlotType, BaseItem>();
    }

    // Kiểm tra xem vị trí đó có trống và vật phẩm có đúng loại không
    public abstract bool TryEquip(BaseItem item);
    public abstract BaseItem Unequip(EquipmentSlotType slot);
    
    // Tính tổng chỉ số từ tất cả trang bị đang mặc
    public abstract Stats GetTotalBonusStats();
    
    // Quét toàn bộ đồ đang mặc để gom nhóm và tính mốc Tộc hệ (Synergy)
    public abstract Dictionary<SynergyType, int> CalculateActiveSynergies();
}