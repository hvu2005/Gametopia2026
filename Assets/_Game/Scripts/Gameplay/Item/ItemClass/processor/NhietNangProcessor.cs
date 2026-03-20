





using System.Collections.Generic;

public class NhietNangProcessor : ItemClassProcessor
{
    public NhietNangProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp(BaseEntity target)
    {
        base.OnMilestoneUp(target);
        if (this.currentMilestone == 1)
        {
            target.Stats.speed += 10;
        }
        else if (this.currentMilestone == 2)
        {
            target.Stats.thorn += 20;

        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {
            target.Stats.speed -= 10;

        }
        else if (this.currentMilestone == 1)
        {
            target.Stats.thorn += 20;

        }
    }

}