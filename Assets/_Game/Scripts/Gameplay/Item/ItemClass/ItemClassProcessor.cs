

using System.Collections.Generic;

public class ItemClassProcessor
{
    private int _count = 0;

    public List<int> milestoneList = new List<int>();

    public int currentMilestone = 0;

    public ItemClassProcessor(List<int> milestoneList)
    {
        this.milestoneList = milestoneList;
    }

    public int count
    {
        get { return _count; }
        set
        {
            // if (count == milestoneList[currentMilestone])
            // {
            //     currentMilestone++;
            //     this.OnMilestoneUp();
            // }
            // else if (count < milestoneList[currentMilestone])
            // {
            //     currentMilestone--;
            //     this.OnMilestoneDown();
            // }

            _count = value;
        }
    }

    public virtual void OnMilestoneUp()
    {

    }

    public virtual void OnMilestoneDown()
    {
        
    }

}
