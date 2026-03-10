using UnityEngine;

/// <summary>
/// Cấu hình xác suất Drop Item (Star Level, Rarity).
/// Đặt trên WorldItem hoặc Enemy để kiểm soát loot table.
/// </summary>
[System.Serializable]
public class DropConfig
{
    [Header("Star Range")]
    [Range(0, 3)] public int MinStar = 0;
    [Range(0, 3)] public int MaxStar = 1;

    [Header("Rarity Weights (tổng không cần = 100)")]
    public float NormalWeight   = 60f;
    public float RareWeight     = 25f;
    public float EpicWeight     = 12f;
    public float LegendaryWeight = 3f;

    /// <summary>Random Rarity theo trọng số đã cấu hình.</summary>
    public RarityType RollRarity()
    {
        float total = NormalWeight + RareWeight + EpicWeight + LegendaryWeight;
        float roll = Random.Range(0f, total);

        if (roll < NormalWeight)    return RarityType.Normal;
        roll -= NormalWeight;
        if (roll < RareWeight)      return RarityType.Rare;
        roll -= RareWeight;
        if (roll < EpicWeight)      return RarityType.Epic;
        return RarityType.Legendary;
    }
}
