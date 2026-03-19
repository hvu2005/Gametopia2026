
using System.Collections.Generic;
using UnityEngine;

public class MagicDamageProcessor : BaseStatProcessor, IOnAttack
{
    public void ProcessOnAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        Debug.Log("Processing Magic Damage" + source.Stats.magicDamage);
        var damage = source.Stats.magicDamage;
        target.currentHp -= damage;
        source.lastDamageDealt = damage;

        EventBus.Emit(BattleEventType.SpawnFloatingText, new FloatingTextEventData
        {
            Target = target,
            Value = damage,
            Type = FloatingTextType.MagicDamage,
            OffsetBuffer = Vector2.zero
        });
    }
}