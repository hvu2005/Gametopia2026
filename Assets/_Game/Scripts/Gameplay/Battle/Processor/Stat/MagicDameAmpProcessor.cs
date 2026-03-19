using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Khuếch đại sát thương phép (increaseMagic): nhân magicDamage thêm % trước khi đánh.
/// Ví dụ: increaseMagic = 50 → magicDamage × 1.5
/// Pattern giống CritProcessor: lưu magicDamage     gốc ở PreAttack, hoàn trả ở PostAttack.
/// </summary>
public class MagicDamageAmpProcessor : BaseStatProcessor, IPreAttack, IPostAttack
{
    private float originalMagicDamage;

    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.increaseMagic > 0)
        {
            originalMagicDamage = source.Stats.magicDamage;
            source.Stats.magicDamage *= 1f + source.Stats.increaseMagic / 100f;
            Debug.Log($"[MagicDamageAmp] {source.name} +{source.Stats.increaseMagic}% → damage: {originalMagicDamage} → {source.Stats.magicDamage}");
        }
    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (originalMagicDamage != 0)
        {
            source.Stats.magicDamage = originalMagicDamage;
            originalMagicDamage = 0;
        }
    }
}
