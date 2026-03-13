using UnityEngine;

/// <summary>
/// Hút máu (suckBlood): chuyển hóa % sát thương vừa gây ra thành máu hồi cho người đánh.
/// Ví dụ: suckBlood = 30 → gây 100 sát thương → hồi 30 HP.
/// Đọc lastDamageDealt được ghi bởi PhysicalDamageProcessor.
/// </summary>
public class SuckBloodProcessor : BaseStatProcessor, IPostAttack
{
    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        if (source.Stats.suckBlood <= 0) return;
        if (source.lastDamageDealt <= 0) return;

        float healAmount = source.lastDamageDealt * (source.Stats.suckBlood / 100f);
        source.Heal(healAmount);
        Debug.Log($"[SuckBlood] {source.name} hút {healAmount:F1} máu ({source.Stats.suckBlood}% của {source.lastDamageDealt:F1} sát thương)");
    }
}
