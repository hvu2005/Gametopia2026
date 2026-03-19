

using System.Collections.Generic;
using UnityEngine;

public class ItemClassProcessor
{
    private int _count = 0;

    public List<int> milestoneList;

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
            _count = value;



        }
    }

    public void CheckMilestone(BaseEntity target)
    {
        if (_count == milestoneList[currentMilestone])
        {
            currentMilestone++;
            this.OnMilestoneUp(target);
        }
        else if (currentMilestone > 0 && _count < milestoneList[currentMilestone])
        {
            currentMilestone--;
            this.OnMilestoneDown(target);
        }
    }

    public virtual void OnMilestoneUp(BaseEntity target)
    {

    }

    public virtual void OnMilestoneDown(BaseEntity target)
    {

    }

}
