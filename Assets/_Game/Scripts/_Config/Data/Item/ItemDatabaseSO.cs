using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private List<ItemDefinitionSO> items = new();

    public IReadOnlyList<ItemDefinitionSO> Items => items;

    public ItemDefinitionSO GetRandom()
    {
        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("ItemDatabaseSO: Database rỗng!");
            return null;
        }
        return items[Random.Range(0, items.Count)];
    }

    public ItemDefinitionSO GetByID(string id)
    {
        return items.Find(i => i.ItemID == id);
    }
}
