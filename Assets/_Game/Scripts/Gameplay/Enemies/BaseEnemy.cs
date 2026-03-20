using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseEnemy : BaseEntity
{
    public bool canSelect = true;
    public bool isHoving = false;

    private Vector3 originScale;

    public override void Start()
    {
        // base.Start();
        this.originScale = this.visual.transform.localScale;
    }

    public void Init(EnemyDataSO enemyData, int level = 1)
    {
        this.Stats = enemyData.Stats;
        this.Stats.hp = (float)Math.Round(this.Stats.hp * (1f + (level - 1) / 6f), 1);
        this.Stats.physicalDamage = (float)Math.Round(this.Stats.physicalDamage * (1f + (level - 1) / 16f), 1);
        this.Stats.poisonous = (float)Math.Round(this.Stats.poisonous * (1f + (level - 1) / 16f), 1);

        this.visual.sprite = enemyData.Sprite;
        this.currentHp = Stats.hp;
        this.currentArmor = Stats.armor;
        this.OnUpdateStat();
    }

    public override void OnUpdateStat()
    {
        base.OnUpdateStat();

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();

        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySfx(AudioController.AudioKeys.SfxEnemyDie);
        }

        Debug.Log($"{name} has died.");

        visual.DOFade(0f, 0.25f)
            .OnComplete(() => { gameObject.SetActive(false); });
    }

    public void OnMouseDown()
    {
        
        if (!canSelect) return;

        EventBus.Emit<BaseEnemy>(EnemyEventType.Select, this);
    }

    public void OnMouseEnter()
    {
        EventBus.Emit<Stats>(UIEvent.HoverStat, this.Stats);
        if (!canSelect) return;

        this.visual.transform.DOScale(this.originScale * 1.2f, 0.2f);
        this.isHoving = true;
    }

    public void OnMouseOver()
    {
        if (canSelect && !isHoving) return;

        this.visual.transform.DOScale(this.originScale * 1.2f, 0.2f);
        this.isHoving = true;
    }

    public void OnMouseExit()
    {
        this.visual.transform.DOScale(this.originScale, 0.2f);
        this.isHoving = false;
    }
}