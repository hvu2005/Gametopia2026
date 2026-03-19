
using System.Collections.Generic;
using UnityEngine;

public class CritProcessor : BaseStatProcessor, IPreAttack, IPostAttack
{
    public float originalPhysicalDamage;
    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.criticalChance > 0)
        {
            float critRoll = Random.Range(0f, 1f)*100f;
            if (critRoll <= source.Stats.criticalChance)
            {
                originalPhysicalDamage = source.Stats.physicalDamage;
                source.Stats.physicalDamage *= source.Stats.criticalDamage / 100f;
                // Đăng ký entity đã nhảy crit vào hệ thống quản lý
                StatProcessSystem.currentCritEntities.Add(source);
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