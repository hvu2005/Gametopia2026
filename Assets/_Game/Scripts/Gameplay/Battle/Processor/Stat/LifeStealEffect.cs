

using System.Collections.Generic;
using UnityEngine;

public class LifeStealEffect : BaseStatProcessor, IPostAttack
{
    public LifeStealEffect()
    {

    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        source.Heal(source.Stats.lifeSteal);
    }
}
