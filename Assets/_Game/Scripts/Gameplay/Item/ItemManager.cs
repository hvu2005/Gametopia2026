



using System.Collections.Generic;

public class ItemManager
{
    public List<Item> items;

    public void EquipItem(Item item, BaseEntity player)
    {
        player.Stats += item.stats;
    }

    public void UnequipItem(Item item, BaseEntity player)
    {
        player.Stats -= item.stats;
    }
}