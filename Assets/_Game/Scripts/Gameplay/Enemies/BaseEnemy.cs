



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
        this.Stats = enemyData.Stats * (1f + level / 5f);
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
        Debug.Log($"{name} has died.");

        visual.DOFade(0f, 0.25f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

    public void OnMouseDown()
    {
        if (!canSelect) return;

        EventBus.Emit<BaseEnemy>(EnemyEventType.Select, this);
    }

    public void OnMouseEnter()
    {
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