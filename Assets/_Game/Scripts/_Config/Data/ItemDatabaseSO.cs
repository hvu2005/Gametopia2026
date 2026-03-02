using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private List<ItemDataSO> items = new();

    public IReadOnlyList<ItemDataSO> Items => items;

    public ItemDataSO GetRandom()
    {
        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("ItemDatabaseSO: Database rỗng!");
            return null;
        }
        return items[Random.Range(0, items.Count)];
    }

    public ItemDataSO GetByID(string id)
    {
        return items.Find(i => i.ItemID == id);
    }
}
