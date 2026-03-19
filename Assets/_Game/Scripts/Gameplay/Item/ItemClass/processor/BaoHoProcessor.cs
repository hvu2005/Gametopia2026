


using System.Collections.Generic;

public class BaoHoProcessor : ItemClassProcessor
{
    public BaoHoProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.armor += 20;
        }
        if (this.currentMilestone == 2)
        {
            // if(target.GetEffect<BaoHoE>)
            target.Stats.dodgeChance += 10;
        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.armor -= 20;

        }
        if (this.currentMilestone == 1)
        {
            target.Stats.dodgeChance -= 10;

        }
    }


}