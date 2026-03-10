using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý logic TRANG BỊ — kiểm tra AllowedSlot, kích hoạt/tắt BaseItemEffect,
/// và thông báo cho TraitProcessor khi cấu hình trang bị thay đổi.
///
/// Single Responsibility: class này KHÔNG lưu trữ túi đồ (PlayerInventory lo),
/// chỉ quản lý 5 slot trang bị đang mặc.
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;

    /// <summary>Slot đang mặc: Key = EquipmentSlotType, Value = ItemInstance đang đeo</summary>
    private readonly Dictionary<EquipmentSlotType, ItemInstance> _equipped = new();

    // ─── Public API ──────────────────────────────────────────────────────────

    /// <summary>
    /// Trang bị item vào đúng slot AllowedSlot của nó.
    /// Nếu slot đã có item → unequip cái cũ trước.
    /// </summary>
    public bool TryEquip(ItemInstance item)
    {
        if (item == null || item.Definition == null)
        {
            Debug.LogWarning("EquipmentManager.TryEquip: item hoặc definition null");
            return false;
        }

        EquipmentSlotType slot = item.AllowedSlot;

        // Unequip item cũ trong slot nếu có
        if (_equipped.TryGetValue(slot, out ItemInstance current) && current != null)
        {
            UnequipSlot(slot);
        }

        // Equip item mới
        _equipped[slot] = item;

        // Kích hoạt tất cả Effect của item
        foreach (var effect in item.Definition.Effects)
        {
            if (effect != null)
                effect.OnEquip(item, player);
        }

        EventBus.Emit(ItemEventType.OnInventoryChanged, (int)slot);
        Debug.Log($"✅ Equipped [{item.DisplayName}] vào slot [{slot}]");
        return true;
    }

    /// <summary>
    /// Gỡ item khỏi slot chỉ định.
    /// Trả về ItemInstance vừa gỡ (null nếu slot trống).
    /// </summary>
    public ItemInstance Unequip(EquipmentSlotType slot)
    {
        return UnequipSlot(slot);
    }

    /// <summary>Lấy item đang mặc ở slot chỉ định (null nếu trống).</summary>
    public ItemInstance GetEquipped(EquipmentSlotType slot)
    {
        _equipped.TryGetValue(slot, out var item);
        return item;
    }

    /// <summary>Duyệt qua tất cả item đang mặc (dùng cho TraitProcessor).</summary>
    public IEnumerable<ItemInstance> GetAllEquipped()
    {
        foreach (var kv in _equipped)
            if (kv.Value != null) yield return kv.Value;
    }

    public bool IsSlotOccupied(EquipmentSlotType slot) =>
        _equipped.TryGetValue(slot, out var item) && item != null;

    // ─── Internal ────────────────────────────────────────────────────────────

    private ItemInstance UnequipSlot(EquipmentSlotType slot)
    {
        if (!_equipped.TryGetValue(slot, out ItemInstance item) || item == null)
            return null;

        // Tắt tất cả Effect
        foreach (var effect in item.Definition.Effects)
        {
            if (effect != null)
                effect.OnUnequip(item, player);
        }

        _equipped[slot] = null;
        EventBus.Emit(ItemEventType.OnInventoryChanged, (int)slot);
        Debug.Log($"🗑️ Unequipped [{item.DisplayName}] khỏi slot [{slot}]");
        return item;
    }
}
