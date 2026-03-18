using UnityEngine;

/// <summary>
/// HiddenStat - Cơ khí: áp dụng choáng lên mục tiêu.
/// Việc "đòn đánh thứ 3" được xử lý ở Processor, effect này chỉ chịu trách nhiệm gắn choáng.
/// </summary>
public class CoKhiStunEffect : BaseEffect
{
    public override void ApplyEffect(BaseEntity target)
    {
        if (target == null || target.IsDead) return;

        // Tránh add trùng StunEffect.
        if (target.GetEffect<StunEffect>() == null)
        {
            target.ActiveEffects.Add(new StunEffect());
        }

        Debug.Log($"[Effect:CoKhi] {target.name} bị choáng (StunEffect)");
    }
}
