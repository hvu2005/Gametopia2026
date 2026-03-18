using UnityEngine;

/// <summary>
/// Khuếch đại sát thương (increaseDamage): nhân physicalDamage thêm % trước khi đánh.
/// Ví dụ: increaseDamage = 50 → physicalDamage × 1.5
/// Pattern giống CritProcessor: lưu physicalDamage gốc ở PreAttack, hoàn trả ở PostAttack.
/// </summary>
public class DamageAmpProcessor : BaseStatProcessor, IPreAttack, IPostAttack
{
    private float originalPhysicalDamage;

    public void ProcessPreAttack(BaseEntity source, BaseEntity target)
    {
        if (source.Stats.increaseDamage > 0)
        {
            originalPhysicalDamage = source.Stats.physicalDamage;
            source.Stats.physicalDamage *= 1f + source.Stats.increaseDamage / 100f;
            Debug.Log($"[DamageAmp] {source.name} +{source.Stats.increaseDamage}% → damage: {originalPhysicalDamage} → {source.Stats.physicalDamage}");
        }
    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        if (originalPhysicalDamage != 0)
        {
            source.Stats.physicalDamage = originalPhysicalDamage;
            originalPhysicalDamage = 0;
        }
    }
}
