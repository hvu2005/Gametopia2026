


using System.Collections.Generic;

public class TaDienProcessor : ItemClassProcessor
{
    public TaDienProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.hiddenStats.taDien = 1;
        }
        else if (this.currentMilestone == 2)
        {
            target.Stats.lifeSteal += 5;
        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.hiddenStats.taDien = 0;

        }
        else if (this.currentMilestone == 1)
        {
            target.Stats.lifeSteal -= 5;

        }
    }


}