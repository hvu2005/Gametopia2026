



public class XayDungArmorEffect : BaseEffect, IPreEffect
{
    int count = 0;
    public override void ApplyEffect(BaseEntity target)
    {
        if (count++ <= 5)
        {
            target.currentArmor += 5;
        }
    }

    public void ApplyPreEffect(BaseEntity target)
    {
        this.ApplyEffect(target);
    }
}