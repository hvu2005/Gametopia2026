

using UnityEngine;


public class PhysicalDamageProcessor : StatProcessor, IOnAttack
{
    public void ProcessOnAttack(BaseEntity source, BaseEntity target)
    {
        Debug.Log("Processing Physical Damage" + source.Stats.physicalDamage);
        var damage = source.Stats.physicalDamage;
        target.currentHp -= damage;
    }
}