


using System;
using UnityEngine;

public class PoisonEffect : BaseEffect, IPostEffect
{
    public int count = 0;
    public int penalty = 6;
    public override void ApplyEffect(BaseEntity target)
    {
        if (count <= 0) return;

        target.TakeDamage(count);
        Debug.Log($"{target.name} took {count} poison damage! Remaining stacks: {count - 1}");

        EventBus.Emit(BattleEventType.SpawnFloatingText, new FloatingTextEventData
        {
            Target = target,
            Value = count,
            Type = FloatingTextType.Poison,
            OffsetBuffer = Vector2.zero
        });

        target.OnTakeDamage();

        count -= Math.Min(count, penalty);
    }

    public void ApplyPostEffect(BaseEntity target)
    {
        this.ApplyEffect(target);
    }

    public override void TryRemoveEffect(BaseEntity target)
    {
        base.TryRemoveEffect(target);
        if (count <= 0)
        {
            target.ActiveEffects.Remove(this);
        }
    }
}