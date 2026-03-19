using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Interface cho các hành động chiến đấu cơ bản
public interface ICombatant
{
    void OnTakeDamage();
    void PerformBasicAttack(BaseEntity target);
}

public interface ITurnBased
{
    // Gọi mỗi khi bắt đầu/kết thúc lượt (Turn-based)
    void OnTurnStart();
    void OnTurnUpdate();
    void OnTurnEnd();
}

public abstract class BaseEntity : MonoBehaviour, ICombatant
{
    public HiddenStats hiddenStats;
    public Stats Stats;
    public bool IsDead => currentHp <= 0;
    public bool IsAttacked { get; set; } = false;

    public float currentHp;
    public float lastDamageDealt;
    public float currentArmor;

    public bool luckyDropBonus;

    public SpriteRenderer hpBar;

    public SpriteRenderer visual;
    private Color originalColor;

    public List<BaseEffect> ActiveEffects = new();

    public virtual void Awake()
    {
        this.originalColor = new Color(visual.color.r, visual.color.g, visual.color.b, visual.color.a);
        this.currentHp = Stats.hp;
        this.currentArmor = Stats.armor;
    }

    public virtual void Start()
    {
        this.OnUpdateStat();
    }

    public virtual void OnUpdateStat()
    {
        SetHpFill(currentHp / Stats.hp);
    }

    public virtual void Die()
    {
        Debug.Log($"{name} has died.");
        // Thêm hiệu ứng chết, rơi đồ, v.v. ở đây
    }

    public void SetHpFill(float amount)
    {
        if (hpBar)
        {
            var max = Math.Max(0, amount);
            hpBar.transform.localScale = new Vector3(max, 1, 1);
        }
    }

    public T GetEffect<T>() where T : BaseEffect
    {
        return ActiveEffects.Find(e => e is T) as T;
    }

    public virtual void Heal(float amount)
    {
        currentHp = Mathf.Min(currentHp + amount, Stats.hp);
    }
    // Các hành động trong Combat
    public virtual void OnTakeDamage()
    {
        var mat = visual.material;

        mat.DOFloat(0.7f, "_FloatAmount", 0.1f)
            .OnComplete(() =>
            {
                mat.DOFloat(0f, "_FloatAmount", 0.05f);
            });
    }
    public virtual void PerformBasicAttack(BaseEntity target)
    {
        // target.TakeDamage(Stats.physicalDamage, false);
    }
    // public abstract void ApplyStats(Stats stats);
    // public abstract void RemoveStats(Stats stats);

    public void IncreaseStats(Stats stats)
    {
        this.Stats += stats;
        this.OnUpdateStat();
    }

    public void DecreaseStats(Stats stats)
    {
        this.Stats -= stats;
        this.OnUpdateStat();

    }


    public void SetActiveTurn(bool isActive)
    {
        if (!visual) return;

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