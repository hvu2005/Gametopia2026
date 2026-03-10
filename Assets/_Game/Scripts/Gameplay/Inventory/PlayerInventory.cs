using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Túi đồ của Player — chịu trách nhiệm LƯU TRỮ danh sách ItemInstance.
/// Việc Equip/Unequip (mặc đồ, kiểm tra slot, kích hoạt Effect) thuộc về EquipmentManager.
/// </summary>
public class PlayerInventory : BaseInventory
{
    public const int MaxSlots = 6;

    private ItemInstance[] _items;

    public int ItemCount { get; private set; }

    // ─── Read Access ─────────────────────────────────────────────────────────

    public ItemInstance GetItemAt(int index)
    {
        if (index < 0 || index >= MaxSlots) return null;
        return _items[index];
    }

    public IReadOnlyList<ItemInstance> GetAllItems() => _items;

    public bool HasEmptySlot()
    {
        for (int i = 0; i < MaxSlots; i++)
            if (_items[i] == null) return true;
        return false;
    }

    public int GetFirstEmptySlot()
    {
        for (int i = 0; i < MaxSlots; i++)
            if (_items[i] == null) return i;
        return -1;
    }

    // ─── Write Access ────────────────────────────────────────────────────────

    /// <summary>
    /// Thêm item vào ô trống đầu tiên.
    /// Trả về slotIndex (≥0) nếu thành công, -1 nếu đầy.
    /// </summary>
    public bool TryAddItem(ItemInstance item, out int slotIndex)
    {
        slotIndex = GetFirstEmptySlot();
        if (slotIndex < 0)
        {
            Debug.Log("⚠️ PlayerInventory: Inventory đầy!");
            return false;
        }

        _items[slotIndex] = item;
        ItemCount++;

        EventBus.Emit(ItemEventType.OnItemPickedUp, item);
        EventBus.Emit(ItemEventType.OnInventoryChanged, slotIndex);
        return true;
    }

    public ItemInstance RemoveItemAt(int index)
    {
        if (index < 0 || index >= MaxSlots || _items[index] == null)
            return null;

        var removed = _items[index];
        _items[index] = null;
        ItemCount--;

        EventBus.Emit(ItemEventType.OnItemRemoved, removed);
        EventBus.Emit(ItemEventType.OnInventoryChanged, index);
        return removed;
    }

    // ─── Unity Lifecycle ─────────────────────────────────────────────────────

    private void Awake()
    {
        _items = new ItemInstance[MaxSlots];
        ItemCount = 0;
    }
}
