







using System.Collections.Generic;

public class SacLemProcessor : ItemClassProcessor
{
    public SacLemProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.criticalDamage += 50;
        }
        else if (this.currentMilestone == 2)
        {
            target.Stats.physicalDamage += 15;
        }

        target.OnUpdateStat();
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.criticalDamage-= 50;

        }
        else if (this.currentMilestone == 1)
        {
            target.Stats.physicalDamage -= 15;

        }

        target.OnUpdateStat();

    }


}