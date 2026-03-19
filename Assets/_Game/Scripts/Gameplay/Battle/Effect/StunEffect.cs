


using UnityEngine;

public class StunEffect : BaseEffect, IPreEffect
{
    public override void ApplyEffect(BaseEntity target)
    {
        Debug.Log($"{target.name} is stunned!");
        target.IsAttacked = true;
    }

    public void ApplyPreEffect(BaseEntity target)
    {
        this.ApplyEffect(target);
    }

    public override void TryRemoveEffect(BaseEntity target)
    {
        if (target == null) return;
        target.ActiveEffects.Remove(this);
    }
}