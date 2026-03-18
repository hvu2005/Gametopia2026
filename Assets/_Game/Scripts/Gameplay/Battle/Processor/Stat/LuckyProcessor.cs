using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// May mắn (lucky): khi kẻ địch chết sau đòn đánh, tăng tỉ lệ nhận được đồ có bậc hiếm hơn.
/// lucky là % → ví dụ lucky = 40 nghĩa là 40% cơ hội tăng bậc rarity của loot một cấp.
/// 
/// Processor này đặt cờ <see cref="BaseEntity.luckyDropBonus"/> = true lên target khi chết.
/// Hệ thống GenerateLootDrop() trong RunManager sẽ đọc cờ này để roll rarity cao hơn.
/// </summary>
public class LuckyProcessor : BaseStatProcessor, IPostAttack
{
    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.lucky <= 0) return;
        if (!target.IsDead) return;

        float luckyRoll = Random.Range(0f, 1f) * 100f;
        if (luckyRoll <= source.Stats.lucky)
        {
            target.luckyDropBonus = true;
            Debug.Log($"[Lucky] {source.name} kích hoạt may mắn! (roll {luckyRoll:F1} ≤ {source.Stats.lucky}) → loot của {target.name} sẽ có bậc cao hơn");
        }
    }
}
