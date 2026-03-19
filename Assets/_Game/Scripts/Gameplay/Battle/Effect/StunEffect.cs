


using UnityEngine;

public class StunEffect : BaseEffect
{
    public override void ApplyEffect(BaseEntity target)
    {
        Debug.Log($"{target.name} is stunned!");
        target.IsAttacked = true;
    }
}