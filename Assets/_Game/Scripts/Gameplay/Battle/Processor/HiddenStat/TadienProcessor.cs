




using System.Collections.Generic;

public class TadienStatProcessor : BaseStatProcessor, IPostAttack
{

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source == null || source.IsDead) return;

        // HiddenStat Tá điền: chỉ cần gọi effect tương ứng
        
        if(target.GetEffect<TaDienEffect>() == null)
        {
            target.ActiveEffects.Add(new TaDienEffect());
        }
    }
}