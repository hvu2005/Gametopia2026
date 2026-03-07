


using UnityEngine;

public class PoisonEffect : BaseEffect
{
    public int count = 0;
    public int penalty = 6;
    public override void ApplyEffect(BaseEntity target)
    {
        Debug.Log($"{target.name} is poisoned!");
    }
}//