using UnityEngine;


public class StunEffect : StatProcessor, IPostAttack, IProcEffect
{
    public StunEffect()
    {

    }
    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
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
                stun = new StunEffect();
                target.ActiveEffects.Add(stun);
            }
        }
    }

}