


using System.Collections.Generic;

public class DienNangProcessor : ItemClassProcessor
{
    public DienNangProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.poisonous += 5;
        }
        if (this.currentMilestone == 2)
        {
            target.Stats.poisonous += 20;

        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.poisonous -= 5;

        }
        if (this.currentMilestone == 1)
        {
            target.Stats.poisonous -= 20;

        }
    }


}