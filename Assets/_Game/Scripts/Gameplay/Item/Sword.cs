using System.Collections.Generic;
using Unity.VisualScripting;

public class Sword : BaseItem
{
    void Awake()
    {
        ItemID = "sword_001";
        ItemName = "Basic Sword";
        Tier = 1;
        Type = ItemType.Weapon;
        AllowedSlot = EquipmentSlotType.Weapon;
        SetBonus = SynergyType.Warrior;
        
        Stats = new Stats
        {
            atk = 10f,
        };
    }

    // public override bool TryMergeWith(BaseItem otherItem)
    // {
    //     return base.TryMergeWith(otherItem);
    // }

    public override void OnEquip(BaseEntity entity)
    {
        // Cộng stat khi trang bị
        entity.ApplyStats(new Stats
        {
            atk = Stats.atk
        });
        base.OnEquip(entity);
    }

    public override void OnUnequip(BaseEntity entity)
    {
        // Trừ stat khi tháo trang bị
        entity.RemoveStats(new Stats
        {
            atk = Stats.atk
        });
        base.OnUnequip(entity);
    }

    public override bool TryMergeWith(BaseItem otherItem)
    {
        throw new System.NotImplementedException();
    }
}