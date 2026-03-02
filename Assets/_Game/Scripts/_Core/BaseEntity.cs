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
    public bool IsDead => currentHp <= 0;
    public bool IsAttacked { get; set; } = false;

    public float currentHp;

    public SpriteRenderer visual;
    private Color originalColor;

    public List<StatProcessor> ActiveEffects = new List<StatProcessor>();

    public void Awake()
    {
        this.originalColor = new Color(visual.color.r, visual.color.g, visual.color.b, visual.color.a);
        this.currentHp = Stats.hp;
    }

    public T GetEffect<T>() where T : StatProcessor
    {
        return ActiveEffects.Find(e => e is T) as T;
    }

    public virtual void Heal(float amount)
    {
        currentHp = Mathf.Min(currentHp + amount, Stats.hp);
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

    public void SetActiveTurn(bool isActive)
    {
        if(!visual) return;

        if (isActive)
        {
            // Bình thường
            visual.color = new Color(originalColor.r, originalColor.g, originalColor.b);
        }
        else
        {
            // Làm xám + không trong suốt
            visual.color = new Color(
                visual.color.r * 0.5f,
                visual.color.g * 0.5f,
                visual.color.b * 0.5f
            );
        }
    }
}