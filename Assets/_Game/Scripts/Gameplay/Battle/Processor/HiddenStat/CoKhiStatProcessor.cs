




using System.Collections.Generic;
using UnityEngine;

public class CoKhiStatProcessor : BaseStatProcessor, IPostAttack
{

    // Đếm số lần source đã thực hiện đòn đánh (theo vòng đời processor).

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if(source.hiddenStats.coKhi <= 0) return;

        if(target.GetEffect<CoKhiStunEffect>() == null)
        {
            target.ActiveEffects.Add(new CoKhiStunEffect());
        }
    }
}