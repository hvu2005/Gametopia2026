using UnityEngine;

/// <summary>
/// Factory tạo ItemInstance từ ItemDefinitionSO.
/// Tập trung toàn bộ logic khởi tạo item — không cần `new ItemInstance(...)` rải rác.
/// </summary>
public static class ItemFactory
{
    /// <summary>Tạo ItemInstance với Star và Rarity cụ thể.</summary>
    public static ItemInstance Create(ItemDefinitionSO definition, int starLevel = 0, RarityType rarity = RarityType.Normal)
    {
        if (definition == null)
        {
            Debug.LogError("ItemFactory.Create: definition is null!");
            return null;
        }

        return new ItemInstance(definition, starLevel, rarity);
    }

    /// <summary>Tạo ItemInstance với Star và Rarity được random theo DropConfig.</summary>
    public static ItemInstance CreateRandom(ItemDefinitionSO definition, DropConfig dropConfig)
    {
        if (definition == null)
        {
            Debug.LogError("ItemFactory.CreateRandom: definition is null!");
            return null;
        }

        int star = Random.Range(dropConfig.MinStar, dropConfig.MaxStar + 1);
        RarityType rarity = dropConfig.RollRarity();

        return Create(definition, star, rarity);
    }

    /// <summary>
    /// Restore ItemInstance từ dữ liệu Save Game.
    /// Quan trọng: truyền lại existingGuid để Modifier vẫn đúng source.
    /// </summary>
    public static ItemInstance Restore(string existingGuid, ItemDefinitionSO definition, int starLevel, RarityType rarity)
    {
        if (definition == null)
        {
            Debug.LogError("ItemFactory.Restore: definition is null!");
            return null;
        }

        return new ItemInstance(existingGuid, definition, starLevel, rarity);
    }
}
