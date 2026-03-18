using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Né đòn (dodgeChance): tỉ lệ % né tránh toàn bộ đòn đánh của kẻ tấn công.
/// Nếu né thành công → set physicalDamage của attacker = 0 để PhysicalDamageProcessor gây 0 sát thương.
/// Pattern giống CritProcessor: lưu physicalDamage gốc ở PreAttack, hoàn trả ở PostAttack.
/// </summary>
public class DodgeProcessor : BaseStatProcessor, IPreAttack, IPostAttack
{
    private float originalPhysicalDamage;

    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (target.Stats.dodgeChance > 0)
        {
            float dodgeRoll = Random.Range(0f, 1f) * 100f;
            if (dodgeRoll <= target.Stats.dodgeChance)
            {
                originalPhysicalDamage = source.Stats.physicalDamage;
                source.Stats.physicalDamage = 0;
                Debug.Log($"[Dodge] {target.name} né thành công đòn đánh của {source.name}! (roll {dodgeRoll:F1} ≤ {target.Stats.dodgeChance})");
            }
        }
    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (originalPhysicalDamage != 0)
        {
            source.Stats.physicalDamage = originalPhysicalDamage;
            originalPhysicalDamage = 0;
        }
    }
}
