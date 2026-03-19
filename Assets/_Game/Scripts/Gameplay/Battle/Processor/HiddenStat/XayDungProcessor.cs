




using System.Collections.Generic;

public class XayDungStatProcessor : BaseStatProcessor, IPreAttack
{

    public void ProcessPreAttack(BaseEntity attacker, BaseEntity defender, List<BaseEntity> allAliveEnemies = null)
    {
        if (defender == null || defender.IsDead) return;

        if(defender.hiddenStats.xayDung > 0)
        {
            if(defender.GetEffect<XayDungArmorEffect>() == null)
            {

            }
        }
    }
}