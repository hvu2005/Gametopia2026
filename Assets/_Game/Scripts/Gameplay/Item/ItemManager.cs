



using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class ItemManager : EventEmitter
{
    public List<ItemDataSO> itemDataList;
    public List<InventorySlot> itemSlots;
    public List<EquipeSlot> equipeSlots;
    public List<ItemClass> itemClasses;
    public Item itemPrefab;

    private List<ItemDataSO> itemNormalList;
    private List<ItemDataSO> itemRareList;
    private List<ItemDataSO> itemEpicList;
    private List<ItemDataSO> itemLegendaryList;



    public ItemClassSystem itemClassSystem;
    public void Init()
    {
        itemNormalList = new List<ItemDataSO>();
        itemRareList = new List<ItemDataSO>();
        itemEpicList = new List<ItemDataSO>();
        itemLegendaryList = new List<ItemDataSO>();

        foreach (var item in itemDataList)
        {
            switch (item.Rarity)
            {
                case RarityType.Normal:
                    itemNormalList.Add(item);
                    break;

                case RarityType.Rare:
                    itemRareList.Add(item);
                    break;

                case RarityType.Epic:
                    itemEpicList.Add(item);
                    break;

                case RarityType.Legendary:
                    itemLegendaryList.Add(item);
                    break;
            }
        }
    }

    public void CheckMilestoneFor(ItemClassType itemClass, BaseEntity target)
    {
        itemClassSystem.itemClassProcessorDict[itemClass].CheckMilestone(target);
    }

    public void AddItemClass(Item item)
    {
        foreach (var itemClass in item.itemClassTypes)
        {
            itemClassSystem.AddItemClass(itemClass);

        }
    }

    public void RemoveItemClass(Item item)
    {
        foreach (var itemClass in item.itemClassTypes)
        {
            itemClassSystem.RemoveItemClass(itemClass);

        }
    }

    public void UpdateItemClasses()
    {
        var sorted = itemClasses
            .OrderByDescending(x =>
                itemClassSystem.itemClassProcessorDict[x.itemClassType].count)
            .ToList();

        // Set lại sibling index
        for (int i = 0; i < sorted.Count; i++)
        {
            sorted[i].transform.SetSiblingIndex(i);
            var count = itemClassSystem.itemClassProcessorDict[sorted[i].itemClassType].count;
            float amount = count > 0f ? 1f : 0.3f;
            sorted[i].SetOpacity(amount);
            sorted[i].countText.text = count + "";
        }
    }

    public void UpdateAddItemClasses(Item item, BaseEntity player)
    {
        foreach (var itemClass in item.itemClassTypes)
        {
            itemClassSystem.AddItemClass(itemClass);
            this.CheckMilestoneFor(itemClass, player);

        }
        this.UpdateItemClasses();
    }

    public void UpdateRemoveItemClasses(Item item, BaseEntity player)
    {
        foreach (var itemClass in item.itemClassTypes)
        {
            itemClassSystem.RemoveItemClass(itemClass);
            this.CheckMilestoneFor(itemClass, player);

        }

        this.UpdateItemClasses();
    }

    public Item SpawnItem(ItemDataSO itemData, Transform parent)
    {
        var spawnedItem = UnityEngine.Object.Instantiate(itemPrefab, parent);
        spawnedItem.Init(itemData);

        foreach (var itemSlot in itemSlots)
        {
            if (!itemSlot.IsEmpty()) continue;

            var rectItem = spawnedItem.GetComponent<RectTransform>();
            var rectSlot = itemSlot.GetComponent<RectTransform>();

            itemSlot.currentItem = spawnedItem;
            spawnedItem.currentSlot = itemSlot;

            rectItem.position = rectSlot.position;

            break;
        }

        return spawnedItem;
    }
    public List<ItemDataSO> GetRandomItemDataList(BaseEntity player, int count = 3)
    {
        HashSet<ItemDataSO> result = new();

        float weight = player.hiddenStats.rarityWeight;
        float luck = player.Stats.luck;

        int safety = 20; // tránh loop vô hạn

        while (result.Count < count && safety-- > 0)
        {
            var rarity = RollRarity(weight, (int)luck, (int)player.hiddenStats.badLuck);
            var item = GetRandomItemByRarity(rarity);

            if (item != null)
                result.Add(item); // HashSet auto chống trùng
        }

        return result.ToList();
    }

    private ItemDataSO GetRandomItemByRarity(RarityType rarity)
    {
        List<ItemDataSO> list = rarity switch
        {
            RarityType.Normal => itemNormalList,
            RarityType.Rare => itemRareList,
            RarityType.Epic => itemEpicList,
            RarityType.Legendary => itemLegendaryList,
            _ => itemNormalList
        };

        if (list == null || list.Count == 0)
            return null;

        return list[UnityEngine.Random.Range(0, list.Count)];
    }


    private RarityType RollRarity(float weight, int luck, int badLuck = 0)
    {
        int bonus = 0;

        if (luck > 0 && UnityEngine.Random.Range(0, 100) < luck) bonus++;
        if (badLuck > 0 && UnityEngine.Random.Range(0, 100) < badLuck) bonus--;

        // 🎯 base tier theo weight
        int tier = 0;
        if (weight >= 20) tier = 1;
        if (weight >= 50) tier = 2;
        if (weight >= 80) tier = 3;

        // 🍀 apply lucky / badLuck
        tier += bonus;

        // 🔒 clamp (0 = Normal, 3 = Legendary)
        tier = Mathf.Clamp(tier, 0, 3);

        return (RarityType)tier;
    }
}