
using System.Collections.Generic;
using UnityEngine;

public class StatSystem : Singleton<StatSystem>
{
    public List<StatProcessor> statProcessors = new();

    void Start()
    {
        statProcessors.Add(new StunEffect());
        statProcessors.Add(new LifeStealEffect());
        statProcessors.Add(new PoisonEffect());

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