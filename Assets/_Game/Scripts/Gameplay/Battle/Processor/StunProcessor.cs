using UnityEngine;


public class StunProcessor : BaseStatProcessor, IPostAttack, IProcEffect
{
    public StunProcessor()
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
                stun = new();
                target.ActiveEffects.Add(stun);
            }
        }
    }

}