


using System.Collections.Generic;

public class CoKhiProcessor : ItemClassProcessor
{
    public CoKhiProcessor(List<int> milestoneList) : base(milestoneList)
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