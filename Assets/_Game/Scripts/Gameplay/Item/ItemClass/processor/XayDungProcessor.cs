


using System.Collections.Generic;

public class XayDungProcessor : ItemClassProcessor
{
    public XayDungProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.thorn += 20;
        }
        else if (this.currentMilestone == 2)
        {
            target.hiddenStats.xayDung = 1;
        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.thorn -= 20;

        }
        else if (this.currentMilestone == 1)
        {
            target.hiddenStats.xayDung = 0;

        }
    }


}