using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// counterAttackChance: % cơ hội phản đòn (ví dụ 25 = 25% chance)
/// Khi kích hoạt → phản lại toàn bộ sát thương vừa nhận (có thể chỉnh % nếu muốn)
/// </summary>
public class CounterAttackProcessor : BaseStatProcessor, IBeAttacked
{

    public void ProcessBeAttacked(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (target.Stats.thorn <= 0) return;
        // if (target.lastDamageDealt <= 0) return;
        if (target.IsDead) return;

        int roll = Random.Range(0, 100);
        if (roll >= target.Stats.thorn) return;

        source.TakeDamage(source.Stats.physicalDamage);

        // source.IsAttacked = false;
        
    }
}