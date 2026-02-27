using System.Collections.Generic;

// Interface cho các hành động chiến đấu cơ bản
public interface ICombatant {
    void TakeDamage(float amount, bool isCritical);
    void PerformBasicAttack(List<ICombatant> availableTargets);
}

public abstract class BaseEntity : ICombatant {
    protected Dictionary<StatType, float> BaseStats;
    public bool IsDead => BaseStats[StatType.HP] <= 0;
    protected List<StatModifier> ActiveModifiers;
    public abstract float GetTotalStat(StatType statType);
    
    // Các hành động trong Combat
    public abstract void TakeDamage(float amount, bool isCritical);
    public abstract void PerformBasicAttack(List<ICombatant> availableTargets);
    public abstract void ApplyStatModifier(StatModifier modifier);
    public abstract void RemoveStatModifier(StatModifier modifier);
    
    // Gọi mỗi khi bắt đầu/kết thúc lượt (Turn-based)
    public abstract void OnTurnStart();
    //Hành động đang diễn ra trong lượt (VD: Buff/Debuff theo thời gian)
    public abstract void OnTurnUpdate();
    public abstract void OnTurnEnd();
}