using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedProcessor : BaseStatProcessor, IPostAttack
{
    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.speed <= 0) return;
        if (target.IsDead) return;

        if (source.hasExtraAttacked) return;

        // 🎲 roll
        int roll = Random.Range(0, 100);
        if (roll >= source.Stats.speed) return;

        source.hasExtraAttacked= true;
        source.IsAttacked = false;

        Debug.Log($"[AttackSpeed] {source.name} được đánh thêm 1 lần!");
    }
}