using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// increaseMagic: % tăng thêm magic damage (ví dụ 50 = +50%)
/// </summary>
public class MagicDamageAmpProcessor : BaseStatProcessor, IPreAttack
{
    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.increaseMagic <= 0) return;

        int bonusMagicDamage = Mathf.RoundToInt(
            source.Stats.magicDamage * (source.Stats.increaseMagic / 100f)
        );

        source.tempBonusMagic += bonusMagicDamage;

        Debug.Log($"[MagicDamageAmp] {source.name} +{source.Stats.increaseMagic}% → +{bonusMagicDamage} magic damage");
    }
}