
using System.Collections.Generic;
using UnityEngine;

public class StatProcessSystem
{
    public List<BaseStatProcessor> statProcessors = new();

    public StatProcessSystem()
    {
        statProcessors.Add(new StunProcessor());
        statProcessors.Add(new LifeStealEffect());
        statProcessors.Add(new PoisonProcessor());
        statProcessors.Add(new PhysicalDamageProcessor());
        statProcessors.Add(new CritProcessor());

    }

    public void ProcessPreAttack(BaseEntity source, BaseEntity target)
    {
        foreach (var p in statProcessors)
        {
            if (p is IPreAttack stage)
                stage.ProcessPreAttack(source, target);
        }
    }

    public void ProcessOnAttack(BaseEntity source, BaseEntity target)
    {
        foreach (var p in statProcessors)
        {
            if (p is IOnAttack stage)
                stage.ProcessOnAttack(source, target);
        }
    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        foreach (var p in statProcessors)
        {
            if (p is IPostAttack stage)
                stage.ProcessPostAttack(source, target);
        }
    }
}