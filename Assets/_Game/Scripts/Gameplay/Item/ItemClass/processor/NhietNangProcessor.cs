





using System.Collections.Generic;

public class NhietNangProcessor : ItemClassProcessor
{
    public NhietNangProcessor(List<int> milestoneList) : base(milestoneList)
    {

    }

    public override void OnMilestoneUp()
    {
        base.OnMilestoneUp();
        if(this.currentMilestone == 1)
        {
            
        }
    }

    public override void OnMilestoneDown()
    {
        base.OnMilestoneDown();
    }


}