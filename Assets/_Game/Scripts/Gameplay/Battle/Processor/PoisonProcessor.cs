
using UnityEngine;

public class PoisonProcessor : BaseStatProcessor, IPostAttack
{
    public PoisonProcessor()
    {

    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        // Không cần xử lý gì trong giai đoạn này
        var poison = target.GetEffect<PoisonEffect>();
        if (poison == null)
        {
            poison = new();
            target.ActiveEffects.Add(poison);
        }
        poison.count += (int)source.Stats.poisonous;
        Debug.Log($"Applied Poison: {poison.count} stacks");
    }
}