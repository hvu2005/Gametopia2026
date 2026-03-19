




using System.Collections.Generic;

public class XayDungStatProcessor : BaseStatProcessor, IBeAttacked
{

    public void ProcessBeAttacked(BaseEntity attacker, BaseEntity defender, List<BaseEntity> allAliveEnemies = null)
    {
        if (defender == null || defender.IsDead) return;

        // HiddenStat Xây dựng: chỉ cần gọi effect tương ứng
        new XayDungArmorEffect().ApplyEffect(defender);
    }
}