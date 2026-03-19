
using System.Collections.Generic;
using UnityEngine;

public class PoisonProcessor : BaseStatProcessor, IPostAttack
{
    public PoisonProcessor()
    {

    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        // Không cần xử lý gì trong giai đoạn này
        var poison = target.GetEffect<PoisonEffect>();
        bool isNewPoison = false;
        if (poison == null)
        {
            poison = new();
            target.ActiveEffects.Add(poison);
            isNewPoison = true;
        }
        // Chỉ tích luỹ độc, không hiển thị floating text tĩnh ngay lập tức
        poison.count += (int)source.Stats.poisonous;
        Debug.Log($"Applied Poison: {poison.count} stacks");
    }
}