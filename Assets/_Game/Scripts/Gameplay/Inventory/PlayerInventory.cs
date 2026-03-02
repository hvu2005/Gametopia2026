using System;
using UnityEngine;

public class PlayerInventory : BaseInventory, IStatProvider
{
    public const int MaxSlots = 6;

    [SerializeField] private ItemDataSO[] equippedItems;

    public int ItemCount { get; private set; }

    public ItemDataSO GetItemAt(int index)
    {
        if (index < 0 || index >= MaxSlots) return null;
        return equippedItems[index];
    }

    public bool TryAddItem(ItemDataSO item, out int slotIndex)
    {
        slotIndex = -1;

        for (int i = 0; i < MaxSlots; i++)
        {
            if (equippedItems[i] == null)
            {
                equippedItems[i] = item;
                slotIndex = i;
                ItemCount++;

                EventBus.Emit(ItemEventType.OnItemPickedUp, item);
                EventBus.Emit(ItemEventType.OnInventoryChanged, slotIndex);
                return true;
            }
        }

        Debug.Log("⚠️ PlayerInventory: Inventory đầy!");
        return false;
    }

    public ItemDataSO RemoveItemAt(int index)
    {
        if (index < 0 || index >= MaxSlots || equippedItems[index] == null)
            return null;

        var removed = equippedItems[index];
        equippedItems[index] = null;
        ItemCount--;

        EventBus.Emit(ItemEventType.OnItemRemoved, removed);
        EventBus.Emit(ItemEventType.OnInventoryChanged, index);
        return removed;
    }

    public bool HasEmptySlot()
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            if (equippedItems[i] == null) return true;
        }
        return false;
    }

    public int GetFirstEmptySlot()
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            if (equippedItems[i] == null) return i;
        }
        return -1;
    }

    public Stats GetBonusStats()
    {
        Stats total = new Stats();
        for (int i = 0; i < MaxSlots; i++)
        {
            if (equippedItems[i] != null)
            {
                total = total + equippedItems[i].BuffStats;
            }
        }
        return total;
    }

    private void Awake()
    {
        equippedItems = new ItemDataSO[MaxSlots];
        ItemCount = 0;
    }
}
