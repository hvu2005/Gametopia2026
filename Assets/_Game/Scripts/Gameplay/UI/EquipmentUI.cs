using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý UI panel trang bị (Equipment Panel).
/// - Đồng bộ hiển thị với EquipmentManager qua OnInventoryChanged EventBus
/// - Chuột phải vào slot → Unequip → đưa item về túi (PlayerInventory + InventoryUI)
/// </summary>
public class EquipmentUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryUI inventoryUI;

    [Header("Equipment Slots")]
    [Tooltip("Danh sách 5 slot UI, mỗi slot phải có EquipmentSlotType khác nhau")]
    [SerializeField] private List<EquipmentSlotUI> equipSlots = new();

    // ─── Unity Lifecycle ─────────────────────────────────────────────────────

    private void Awake()
    {
        foreach (var slot in equipSlots)
            slot.OnSlotRightClicked += OnEquipSlotRightClicked;
    }

    private void OnEnable()
    {
        EventBus.On<int>(ItemEventType.OnInventoryChanged, OnEquipmentChanged);
        RefreshAllSlots(); // đồng bộ khi kích hoạt UI
    }

    private void OnDisable()
    {
        EventBus.Off<int>(ItemEventType.OnInventoryChanged, OnEquipmentChanged);
    }

    // ─── Refresh ─────────────────────────────────────────────────────────────

    /// <summary>Được gọi khi EquipmentManager emit OnInventoryChanged.</summary>
    private void OnEquipmentChanged(int slotInt)
    {
        // Chỉ refresh slot tương ứng
        EquipmentSlotType slotType = (EquipmentSlotType)slotInt;
        RefreshSlot(slotType);
    }

    public void RefreshSlot(EquipmentSlotType slotType)
    {
        EquipmentSlotUI slotUI = FindSlotUI(slotType);
        if (slotUI == null) return;

        ItemInstance item = equipmentManager != null ? equipmentManager.GetEquipped(slotType) : null;
        if (item != null)
            slotUI.SetItem(item);
        else
            slotUI.Clear();
    }

    private void RefreshAllSlots()
    {
        foreach (var slotUI in equipSlots)
            RefreshSlot(slotUI.SlotType);
    }

    // ─── Right-Click: Unequip → Inventory ────────────────────────────────────

    private void OnEquipSlotRightClicked(EquipmentSlotUI slotUI)
    {
        if (slotUI.IsEmpty || equipmentManager == null) return;

        ItemInstance item = slotUI.CurrentItem;
        EquipmentSlotType slotType = slotUI.SlotType;

        // 1. Unequip khỏi EquipmentManager (kích hoạt OnUnequip effect)
        equipmentManager.Unequip(slotType);

        // 2. Đưa item về túi đồ
        if (playerInventory != null && playerInventory.TryAddItem(item, out int bagSlotIndex))
        {
            // 3. Cập nhật InventoryUI để hiện icon trong túi
            if (inventoryUI != null)
                inventoryUI.ForceSetSlot(bagSlotIndex, item);

            Debug.Log($"↩️ Unequip về túi: [{item.Rarity}] {item.DisplayName} → Bag Slot {bagSlotIndex}");
        }
        else
        {
            // Túi đầy → re-equip lại để không mất item
            Debug.LogWarning($"⚠️ Túi đầy! Không thể unequip [{item.DisplayName}]");
            equipmentManager.TryEquip(item);
        }
    }

    // ─── Helpers ─────────────────────────────────────────────────────────────

    private EquipmentSlotUI FindSlotUI(EquipmentSlotType slotType)
    {
        foreach (var s in equipSlots)
            if (s.SlotType == slotType) return s;
        return null;
    }
}
