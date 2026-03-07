
using UnityEngine;

public class PoisonProcessor : BaseStatProcessor, IPostAttack
{
    public int count = 0;
    public int penalty = 6;

    public PoisonProcessor()
    {
        this.count = 0;
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