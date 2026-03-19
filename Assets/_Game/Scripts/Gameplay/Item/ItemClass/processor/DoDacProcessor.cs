





using System.Collections.Generic;

public class DoDacProcessor : ItemClassProcessor
{
    public DoDacProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.dodgeChance += 15;
        }
        if (this.currentMilestone == 2)
        {
            target.Stats.luck += 20;

        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.dodgeChance -= 15;

        }
        if (this.currentMilestone == 1)
        {
            target.Stats.luck -= 20;

        }
    }

}