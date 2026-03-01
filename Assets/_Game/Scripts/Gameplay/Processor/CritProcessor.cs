

using UnityEngine;



public class CritProcessor : StatProcessor, IPreAttack, IPostAttack
{
    public float originalPhysicalDamage;
    public void ProcessPreAttack(BaseEntity source, BaseEntity target)
    {
        if (source.Stats.criticalChance > 0)
        {
            float critRoll = Random.Range(0f, 1f)*100f;
            if (critRoll <= source.Stats.criticalChance)
            {
                originalPhysicalDamage = source.Stats.physicalDamage;
                source.Stats.physicalDamage *= source.Stats.criticalDamage / 100f;

            }
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