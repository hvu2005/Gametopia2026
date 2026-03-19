
using System;
using System.Collections.Generic;

[System.Serializable]
public class ItemClassSystem
{
    public Dictionary<ItemClassType, ItemClassProcessor> itemClassProcessorDict = new Dictionary<ItemClassType, ItemClassProcessor>();
    
    public ItemClassSystem()
    {
        itemClassProcessorDict.Add(ItemClassType.BaoHo, new BaoHoProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.CoKhi, new CoKhiProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.XayDung, new CoKhiProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.TaDien, new CoKhiProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.SacLem, new CoKhiProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.DoDac, new CoKhiProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.DienNang, new CoKhiProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.NhietNang, new CoKhiProcessor(new List<int>() {2, 4}));
        itemClassProcessorDict.Add(ItemClassType.NangDo, new CoKhiProcessor(new List<int>() {2, 4}));
        
    }

    public void AddItemClass(ItemClassType itemClassType)
    {
        itemClassProcessorDict[itemClassType].count++;
    }

    public void RemoveItemClass(ItemClassType itemClassType)
    {
        itemClassProcessorDict[itemClassType].count--;
    }
}