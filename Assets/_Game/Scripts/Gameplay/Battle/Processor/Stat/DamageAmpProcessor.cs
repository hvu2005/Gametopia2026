using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// increaseDamage: % tăng thêm damage (ví dụ 50 = +50%)
/// </summary>
public class DamageAmpProcessor : BaseStatProcessor, IPreAttack
{
    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.increaseDamage <= 0) return;

        int bonusDamage = Mathf.RoundToInt(
            source.Stats.physicalDamage * (source.Stats.increaseDamage / 100f)
        );

        source.tempBonusDamage += bonusDamage;

        Debug.Log($"[DamageAmp] {source.name} +{source.Stats.increaseDamage}% → +{bonusDamage} damage");
    }
}