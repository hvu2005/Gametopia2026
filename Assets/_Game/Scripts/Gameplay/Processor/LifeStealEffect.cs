

using UnityEngine;

public class LifeStealEffect : StatProcessor, IPostAttack
{
    public LifeStealEffect()
    {

    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        source.Heal(source.Stats.lifeSteal);
    }
}