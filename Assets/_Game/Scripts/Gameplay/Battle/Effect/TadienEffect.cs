using UnityEngine;

/// <summary>
/// HiddenStat - Tá điền: hồi 10% HP tối đa mỗi lượt đánh (khi được gọi).
/// </summary>
public class TaDienEffect : BaseEffect, IPostEffect
{
    private readonly float _healPercent;

    public TaDienEffect(float healPercent = 0.10f)
    {
        _healPercent = healPercent;
    }

    public override void ApplyEffect(BaseEntity target)
    {
        if (target == null || target.IsDead) return;

        int healAmount = Mathf.Max(1, (int)(target.Stats.hp * _healPercent));
        if (healAmount <= 0) return;

        float before = target.currentHp;
        target.Heal(healAmount);

        Debug.Log($"[Effect:Tadien] {target.name} hồi {target.currentHp - before:F1} HP ({_healPercent * 100f:F0}% maxHP) → {target.currentHp:F1}/{target.Stats.hp:F1}");
    }

    public void ApplyPostEffect(BaseEntity target)
    {
        this.ApplyEffect(target);
    }
}
