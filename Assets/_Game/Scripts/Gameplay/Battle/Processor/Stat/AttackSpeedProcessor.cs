using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tốc độ đánh (speed): tỉ lệ % đánh thêm 1 đòn nữa sau đòn chính.
/// Đòn phụ dùng physicalDamage đã được hoàn trả về giá trị gốc (sau tất cả PostAttack khác).
/// Pattern giống PoisonProcessor / StunProcessor: chỉ implement IPostAttack.
/// </summary>
public class AttackSpeedProcessor : BaseStatProcessor, IPostAttack
{
    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.speed <= 0) return;
        if (target.IsDead) return;

        float speedRoll = Random.Range(0f, 1f) * 100f;
        if (speedRoll <= source.Stats.speed)
        {
            float extraDamage = source.Stats.physicalDamage;
            target.currentHp -= extraDamage;
            Debug.Log($"[AttackSpeed] {source.name} đánh thêm 1 đòn! (roll {speedRoll:F1} ≤ {source.Stats.speed}) → {extraDamage} sát thương → {target.name} còn {target.currentHp} HP");
        }
    }
}
