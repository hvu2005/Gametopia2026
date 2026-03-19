using System.Collections.Generic;
using UnityEngine;


public class StunProcessor : BaseStatProcessor, IPostAttack, IProcEffect
{
    public StunProcessor()
    {

    }
    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        this.TryApplyEffect(source, target);
    }
    public void TryApplyEffect(BaseEntity attacker, BaseEntity target)
    {
        var stun = target.GetEffect<StunEffect>();
    
        if(Random.value * 100 < attacker.Stats.stunChance)
        {
            if (stun == null)
            {
                stun = new();
                target.ActiveEffects.Add(stun);
                
                EventBus.Emit(BattleEventType.SpawnFloatingText, new FloatingTextEventData
                {
                    Target = target,
                    Value = 0,
                    Type = FloatingTextType.Stun,
                    OffsetBuffer = Vector2.zero
                });
            }
        }
    }

}