using System.Collections.Generic;
using UnityEngine;

// Interface cho các hành động chiến đấu cơ bản
public interface ICombatant {
    void TakeDamage(float amount, bool isCritical);
    void PerformBasicAttack(List<ICombatant> availableTargets);
}

public interface ITurnBased
{
    // Gọi mỗi khi bắt đầu/kết thúc lượt (Turn-based)
    void OnTurnStart();
    void OnTurnUpdate();
    void OnTurnEnd();
}

public abstract class BaseEntity : MonoBehaviour, ICombatant {
    protected Stats BaseStats;
    public bool IsDead => BaseStats.hp <= 0;
    public abstract Stats GetTotalStats();
    
    // Các hành động trong Combat
    public abstract void TakeDamage(float amount, bool isCritical);
    public abstract void PerformBasicAttack(List<ICombatant> availableTargets);
    public abstract void ApplyStats(Stats stats);
    public abstract void RemoveStats(Stats stats);
}