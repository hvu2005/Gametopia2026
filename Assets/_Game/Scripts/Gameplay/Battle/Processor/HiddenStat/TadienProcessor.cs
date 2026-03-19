




using System.Collections.Generic;

public class TadienStatProcessor : BaseStatProcessor, IPostAttack
{

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source == null || source.IsDead) return;

        // HiddenStat Tá điền: chỉ cần gọi effect tương ứng
        
        if(source.hiddenStats.taDien > 0)
        {
            var hp = source.Stats.hp;
            source.Heal(hp/10);
        }
    }
}