
using System;
using System.Collections.Generic;

public class EffectSystem
{

    public EffectSystem()
    {

    }

    public void ApplyEffect(BaseEffect effect, BaseEntity target)
    {
        effect.ApplyEffect(target);
    }

    public void TryRemoveEffect(BaseEffect effect, BaseEntity target)
    {
        effect.TryRemoveEffect(target);
    }

    public void ApplyEffects(BaseEntity target)
    {
        foreach (var effect in target.ActiveEffects)
        {
            ApplyEffect(effect, target);
        }
    }

    public void TryRemoveEffects(BaseEntity target)
    {
        foreach (var effect in target.ActiveEffects)
        {
            TryRemoveEffect(effect, target);
        }
    }

}