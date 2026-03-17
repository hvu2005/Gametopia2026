
public class EquipeSlot : Slot
{

    public override void OnPlaceItem(Item item)
    {
        base.OnPlaceItem(item);
        EventBus.Emit<Item>(ItemEventType.Equipe, item);
    }

    public override void OnRemoveItem(Item item)
    {
        base.OnRemoveItem(item);
        EventBus.Emit<Item>(ItemEventType.Unequipe, item);
    }
}