
using System.Collections.Generic;

[System.Serializable]
public class ItemClassSystem
{
    public Dictionary<ItemClassType, ItemClassProcessor> itemClassProcessorDict = new Dictionary<ItemClassType, ItemClassProcessor>();
    
    public ItemClassSystem()
    {
        
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