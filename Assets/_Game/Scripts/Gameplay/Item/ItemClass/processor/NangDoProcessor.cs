





using System.Collections.Generic;

public class NangDoProcessor : ItemClassProcessor
{
    public NangDoProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.criticalChance += 15;
        }
        if (this.currentMilestone == 2)
        {
            target.Stats.physicalDamage += 20;

        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.criticalChance -= 15;

        }
        if (this.currentMilestone == 1)
        {
            target.Stats.physicalDamage -= 20;

        }
    }


}