



using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class ItemManager : EventEmitter
{
    public List<ItemDataSO> itemDataList;
    public List<InventorySlot> itemSlots;   
    public Item itemPrefab;

    public Item SpawnItem(ItemDataSO itemData, Transform parent)
    {
        var spawnedItem = UnityEngine.Object.Instantiate(itemPrefab, parent);
        spawnedItem.Init(itemData);

        foreach(var itemSlot in itemSlots)
        {
            if(!itemSlot.IsEmpty()) continue;

            var rectItem = spawnedItem.GetComponent<RectTransform>();
            var rectSlot = itemSlot.GetComponent<RectTransform>();

            itemSlot.currentItem = spawnedItem;
            spawnedItem.currentSlot = itemSlot;

            rectItem.position = rectSlot.position;

            break;
        }

        return spawnedItem;    
    }
    public List<ItemDataSO> GetRandomItemDataList(int count = 3)
    {
        return itemDataList
            .OrderBy(x => UnityEngine.Random.value)
            .Take(count)
            .ToList();
    }
}