


using System.Collections.Generic;

public class CoKhiProcessor : ItemClassProcessor
{
    public CoKhiProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.speed += 15;
        }
        if (this.currentMilestone == 2)
        {
            target.hiddenStats.coKhi = 1;
        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.speed -= 15;

        }
        if (this.currentMilestone == 1)
        {
            target.hiddenStats.coKhi = 0;

        }
    }


}