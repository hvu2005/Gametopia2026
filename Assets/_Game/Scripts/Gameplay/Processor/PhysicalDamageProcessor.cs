

using UnityEngine;


public class PhysicalDamageProcessor : StatProcessor, IOnAttack
{
    public void ProcessOnAttack(BaseEntity source, BaseEntity target)
    {
        var damage = source.Stats.physicalDamage;
        target.currentHp -= damage;
        target.IsAttacked = true;
    }
}