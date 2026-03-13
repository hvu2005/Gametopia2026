using UnityEngine;

/// <summary>
/// Phản đòn (counterAttackChance): phản lại % sát thương mà mục tiêu nhận vào về phía kẻ tấn công.
/// Ví dụ: counterAttackChance = 25 → mục tiêu nhận 100 sát thương → kẻ tấn công nhận lại 25 sát thương.
/// Đọc lastDamageDealt (đã được set bởi PhysicalDamageProcessor) để biết sát thương thực tế nhận vào.
/// </summary>
public class CounterAttackProcessor : BaseStatProcessor, IPostAttack
{
    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        if (target.Stats.counterAttackChance <= 0) return;
        if (source.lastDamageDealt <= 0) return;
        if (source.IsDead) return;

        float reflectDamage = source.lastDamageDealt * (target.Stats.counterAttackChance / 100f);
        source.currentHp -= reflectDamage;
        Debug.Log($"[CounterAttack] {target.name} phản lại {reflectDamage:F1} sát thương ({target.Stats.counterAttackChance}% của {source.lastDamageDealt:F1}) về {source.name}");
    }
}
