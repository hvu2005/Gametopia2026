
public class EquipeSlot : Slot
{
    public EquipmentSlotType equipmentSlotType;

    public void Start()
    {
        EventBus.On<bool>(BattleEventType.Start, (_) =>
        {
            if(this.currentItem)
            this.currentItem.canSelect = false;
        });

        EventBus.On<bool>(BattleEventType.End, (_) =>
        {
            if(this.currentItem)
            this.currentItem.canSelect = true;
        });
    }
    public override void OnPlaceItem(Item item)
    {
        base.OnPlaceItem(item);

        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySfx(AudioController.AudioKeys.UiItemDropEquip);
        }

        EventBus.Emit<Item>(ItemEventType.Equipe, item);
    }

    public override void OnRemoveItem(Item item)
    {
        base.OnRemoveItem(item);
        EventBus.Emit<Item>(ItemEventType.Unequipe, item);
    }
}