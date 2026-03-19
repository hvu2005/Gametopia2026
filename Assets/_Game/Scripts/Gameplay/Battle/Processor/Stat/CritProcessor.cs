using System.Collections.Generic;
using UnityEngine;

public class CritProcessor : BaseStatProcessor, IPreAttack
{
    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.criticalChance <= 0) return;

        // 🎲 roll crit (0-99)
        int roll = Random.Range(0, 100);
        if (roll >= source.Stats.criticalChance) return;

        // 💥 tính damage crit (int)
        int bonusDamage = Mathf.RoundToInt(
            source.Stats.physicalDamage * (source.Stats.criticalDamage / 100f)
        );

        source.tempBonusDamage += bonusDamage; // cần biến tạm trong entity

        // đánh dấu crit (nếu bạn vẫn cần UI/effect)
        StatProcessSystem.currentCritEntities.Add(source);

    }
}