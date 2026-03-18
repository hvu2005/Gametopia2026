

using UnityEngine;


public class PhysicalDamageProcessor : BaseStatProcessor, IOnAttack
{
    public void ProcessOnAttack(BaseEntity source, BaseEntity target)
    {
        Debug.Log("Processing Physical Damage" + source.Stats.physicalDamage);
        var damage = source.Stats.physicalDamage;
        target.currentHp -= damage;
        source.lastDamageDealt = damage; // ghi lại để SuckBlood, CounterAttack dùng
    }
}