





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

        }
        else if (this.currentMilestone == 2)
        {

        }
    }

    public override void OnMilestoneDown(BaseEntity target)
    {
        base.OnMilestoneDown(target);
        if (this.currentMilestone == 0)
        {

        }
        else if (this.currentMilestone == 1)
        {

        }
    }

}