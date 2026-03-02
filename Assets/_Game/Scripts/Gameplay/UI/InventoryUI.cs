using System.Collections.Generic;
using UnityEngine;
public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] private List<ItemSlotUI> slots = new();
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerStatManager playerStatManager;

    [Header("Animation Settings")]
    [SerializeField] private float flyDuration = 0.5f;

    public void TryPickUp(WorldItem worldItem)
    {
        if (worldItem == null || worldItem.Data == null) return;
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
            Debug.Log("InventoryUI: Không còn slot trống!");
            return;
        }

        ItemSlotUI targetSlot = slots[emptySlotIndex];
        ItemDataSO itemData = worldItem.Data;

        Vector3 targetPos = targetSlot.WorldPosition;
        worldItem.FlyToSlot(targetPos, flyDuration, () =>
        {
            OnItemArrived(itemData, emptySlotIndex);
        });
    }

    private void OnItemArrived(ItemDataSO itemData, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Count)
        {
            slots[slotIndex].SetItem(itemData);
        }

        if (playerInventory != null)
        {
            playerInventory.TryAddItem(itemData, out _);
        }
        if (playerStatManager != null)
        {
            playerStatManager.Recalculate();
        }

        Debug.Log($"✅ Nhặt item: {itemData.ItemName} → Slot {slotIndex}");
    }

    public void RemoveItemAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count) return;

        slots[slotIndex].Clear();

        if (playerInventory != null)
        {
            playerInventory.RemoveItemAt(slotIndex);
        }

        if (playerStatManager != null)
        {
            playerStatManager.Recalculate();
        }
    }
}
