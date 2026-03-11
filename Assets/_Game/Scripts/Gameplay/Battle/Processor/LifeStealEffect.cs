

using UnityEngine;

public class LifeStealEffect : BaseStatProcessor, IPostAttack
{
    public LifeStealEffect()
    {

    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        source.Heal(source.Stats.lifeSteal);
    }
}
