
using System.Collections.Generic;
using UnityEngine;

public class MagicDamageProcessor : BaseStatProcessor, IOnAttack, IPostAttack
{
    public void ProcessOnAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        Debug.Log("Processing Magic Damage" + source.Stats.magicDamage);
        var damage = source.Stats.magicDamage;
        target.TakeDamage(damage);
        source.lastDamageDealt = damage;

        if (damage > 0)
        {
            EventBus.Emit(BattleEventType.SpawnFloatingText, new FloatingTextEventData
            {
                Target = target,
                Value = damage,
                Type = FloatingTextType.MagicDamage,
                OffsetBuffer = Vector2.zero
            });
        }
    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        source.tempBonusMagic = 0;
    }
}