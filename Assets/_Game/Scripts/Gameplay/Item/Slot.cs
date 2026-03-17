
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item currentItem;
    public virtual bool IsEmpty()
    {
        return currentItem == null;
    }

    public virtual void PlaceItem(Item item)
    {
        currentItem = item;
        this.OnPlaceItem(item);
    }

    public virtual void OnPlaceItem(Item item)
    {
        
    }

    public virtual void OnRemoveItem(Item item)
    {
        
    }

    public virtual void RemoveItem()
    {
        if (currentItem != null)
        {
            this.OnRemoveItem(currentItem);

            currentItem = null;
        }
        
    }


}