using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] private List<ItemSlotUI> slots = new();
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private EquipmentUI equipmentUI; // để refresh slot UI ngay sau khi equip

    [Header("Animation Settings")]
    [SerializeField] private float flyDuration = 0.5f;

    private void Awake()
    {
        // Gán index và đăng ký event click cho từng slot
        for (int i = 0; i < slots.Count; i++)
        {
            int capturedIndex = i;
            slots[i].SetSlotIndex(capturedIndex);
            slots[i].OnSlotClicked      += OnInventorySlotClicked;       // Trái: trang bị
            slots[i].OnSlotRightClicked += OnInventorySlotRightClicked;  // Phải: despawn
        }
    }

    // ─── Pickup Flow ─────────────────────────────────────────────────────────

    /// <summary>Được gọi bởi WorldItem khi người chơi click vào item trên world.</summary>
    public void TryPickUp(WorldItem worldItem)
    {
        if (worldItem == null || worldItem.Instance == null) return;

        int emptySlotIndex = -1;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsEmpty)
            {
                emptySlotIndex = i;
                break;
            }
        }

        if (emptySlotIndex < 0)
        {
            Debug.Log("InventoryUI: Không còn slot trống trong túi!");
            return;
        }

        ItemSlotUI targetSlot = slots[emptySlotIndex];
        ItemInstance instance = worldItem.Instance;

        Vector3 targetPos = targetSlot.WorldPosition;
        worldItem.FlyToSlot(targetPos, flyDuration, () =>
        {
            OnItemArrived(instance, emptySlotIndex);
        });
    }

    private void OnItemArrived(ItemInstance instance, int slotIndex)
    {
        // Hiển thị item trong slot UI
        if (slotIndex >= 0 && slotIndex < slots.Count)
            slots[slotIndex].SetItem(instance);

        // Lưu vào túi đồ (PlayerInventory)
        if (playerInventory != null)
            playerInventory.TryAddItem(instance, out _);

        // KHÔNG tự động trang bị — người chơi phải click slot để equip
        Debug.Log($"🎒 Nhặt vào túi: [{instance.Rarity}] {instance.DisplayName} → Slot {slotIndex}");
    }

    // ─── Equip-from-Slot Flow ─────────────────────────────────────────────────

    /// <summary>Được gọi khi người chơi click vào một slot trong túi đồ.</summary>
    private void OnInventorySlotClicked(ItemSlotUI slot)
    {
        if (slot.IsEmpty || slot.CurrentItem == null)
            return;

        if (equipmentManager == null)
        {
            Debug.LogWarning("InventoryUI: EquipmentManager chưa được gán!");
            return;
        }

        ItemInstance item = slot.CurrentItem;
        bool equipped = equipmentManager.TryEquip(item);

        if (equipped)
        {
            // Gỡ khỏi túi đồ và xóa slot UI
            if (playerInventory != null)
                playerInventory.RemoveItemAt(slot.SlotIndex);
            slot.Clear();

            // Refresh EquipmentUI trực tiếp — không chỉ dùng EventBus để đảm bảo hiển thị đúng
            if (equipmentUI != null)
                equipmentUI.RefreshSlot(item.AllowedSlot);
            else
                Debug.LogWarning("InventoryUI: EquipmentUI chưa được gán — kéo vào field 'Equipment UI' trong Inspector!");

            Debug.Log($"⚔️ Trang bị thành công từ túi: [{item.Rarity}] {item.DisplayName}");
        }
    }

    // ─── Right-Click: Despawn from Inventory ────────────────────────────────

    /// <summary>Chuột phải vào túi đồ → xóa item vĩnh viễn (không quay về world).</summary>
    private void OnInventorySlotRightClicked(ItemSlotUI slot)
    {
        if (slot.IsEmpty || slot.CurrentItem == null) return;

        ItemInstance item = slot.CurrentItem;
        slot.Clear();

        if (playerInventory != null)
            playerInventory.RemoveItemAt(slot.SlotIndex);

        Debug.Log($"🗑️ Despawn khỏi túi: [{item.Rarity}] {item.DisplayName}");
    }

    // ─── ForceSetSlot (dùng bởi EquipmentUI khi unequip về túi) ─────────────

    /// <summary>
    /// Đặt item vào slot chỉ định mà không kích hoạt animation FlyToSlot.
    /// Dùng khi EquipmentUI unequip item về túi đồ.
    /// </summary>
    public void ForceSetSlot(int slotIndex, ItemInstance item)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count) return;
        slots[slotIndex].SetItem(item);
    }

    // ─── Remove Item ─────────────────────────────────────────────────────────

    public void RemoveItemAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count) return;

        ItemInstance instance = slots[slotIndex].CurrentItem;
        slots[slotIndex].Clear();

        if (playerInventory != null)
            playerInventory.RemoveItemAt(slotIndex);

        if (equipmentManager != null && instance != null)
            equipmentManager.Unequip(instance.AllowedSlot);
    }
}
