


using UnityEngine;

public class PoisonEffect : BaseEffect
{
    public int count = 0;
    public int penalty = 6;
    public override void ApplyEffect(BaseEntity target)
    {
        if (count <= 0) return;

        target.currentHp -= count;
        Debug.Log($"{target.name} took {count} poison damage! Remaining stacks: {count - 1}");

        EventBus.Emit(BattleEventType.SpawnFloatingText, new FloatingTextEventData
        {
            Target = target,
            Value = count,
            Type = FloatingTextType.Poison,
            OffsetBuffer = Vector2.zero
        });

        count -= 1;

        if (target.currentHp <= 0)
        {
            target.Die();
        }
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