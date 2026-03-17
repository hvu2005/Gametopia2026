



using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class ItemManager : EventEmitter
{
    public List<Item> items;
    public List<RectTransform> itemSlots;   

    public Item SpawnItem(Item item)
    {
        var spawnedItem = UnityEngine.Object.Instantiate(item);

        return spawnedItem;    
    }

    public List<Item> SpawnRandomItem()
    {
        return items
            .OrderBy(x => UnityEngine.Random.value)
            .Take(3)
            .Select(item => SpawnItem(item))
            .ToList();
    }

    public void OnUpgradeRarity(Item item1, Item item2)
    {
        
    }

    public void OnMergeItems(Item item1, Item item2)
    {
        
    }

    public void OnMoveItemToSlot(Item item, int slotIndex)
    {
        
    }
}