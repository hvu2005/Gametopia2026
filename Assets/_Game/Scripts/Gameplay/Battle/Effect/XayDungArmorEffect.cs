



public class XayDungArmorEffect : BaseEffect
{
    int count = 0;
    public override void ApplyEffect(BaseEntity target)
    {
        if (count++ <= 5)
        {
            target.currentArmor += 5;
        }
    }
}