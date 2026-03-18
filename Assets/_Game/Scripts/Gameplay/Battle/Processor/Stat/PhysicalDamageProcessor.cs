

using System.Collections.Generic;
using UnityEngine;


public class PhysicalDamageProcessor : BaseStatProcessor, IOnAttack
{
    public void ProcessOnAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        Debug.Log("Processing Physical Damage" + source.Stats.physicalDamage);
        var damage = source.Stats.physicalDamage;
        target.currentHp -= damage;
        source.lastDamageDealt = damage; // ghi lại để SuckBlood, CounterAttack dùng
    }
}