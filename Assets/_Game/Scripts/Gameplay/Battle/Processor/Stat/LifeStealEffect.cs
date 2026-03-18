

using System.Collections.Generic;
using UnityEngine;

public class LifeStealProcessor : BaseStatProcessor, IPostAttack
{
    public LifeStealProcessor()
    {

    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        source.Heal(source.Stats.lifeSteal);
    }
}
