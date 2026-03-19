using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// dodgeChance: % né đòn
/// Nếu né → giảm damage attacker xuống 0 bằng bonus âm cực lớn
/// </summary>
public class DodgeProcessor : BaseStatProcessor, IPreAttack
{
    private const int DODGE_NEGATE = -99999999;

    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (target.Stats.dodgeChance <= 0) return;

        // 🎲 roll
        int roll = Random.Range(0, 100);
        if (roll >= target.Stats.dodgeChance) return;

        // 💨 né → triệt tiêu damage
        source.tempBonusDamage += DODGE_NEGATE;

        Debug.Log($"[Dodge] {target.name} né thành công đòn của {source.name}! ({target.Stats.dodgeChance}% chance)");

        EventBus.Emit(BattleEventType.SpawnFloatingText, new FloatingTextEventData
        {
            Target = target,
            Value = 0,
            Type = FloatingTextType.Dodge,
            OffsetBuffer = Vector2.zero,
            customText = "Né"
        });
    }
}