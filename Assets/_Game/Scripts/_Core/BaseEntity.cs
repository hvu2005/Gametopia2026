using System.Collections.Generic;
using UnityEngine;

// Interface cho các hành động chiến đấu cơ bản
public interface ICombatant {
    void TakeDamage(float amount, bool isCritical);
    void PerformBasicAttack(BaseEntity target);
}

public interface ITurnBased
{
    // Gọi mỗi khi bắt đầu/kết thúc lượt (Turn-based)
    void OnTurnStart();
    void OnTurnUpdate();
    void OnTurnEnd();
}

public abstract class BaseEntity : MonoBehaviour, ICombatant {
    public Stats Stats;
    public bool IsDead => Stats.hp <= 0;
    public bool IsAttacked { get; set; } = false;

    public List<StatProcessor> ActiveEffects = new List<StatProcessor>();

    public T GetEffect<T>() where T : StatProcessor
    {
        return ActiveEffects.Find(e => e is T) as T;
    }
    
    // Các hành động trong Combat
    public virtual void TakeDamage(float amount, bool isCritical)
    {
        // Stats.hp -= amount;
    }
    public virtual void PerformBasicAttack(BaseEntity target)
    {
        // target.TakeDamage(Stats.physicalDamage, false);
    }
    // public abstract void ApplyStats(Stats stats);
    // public abstract void RemoveStats(Stats stats);
}