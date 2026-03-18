



using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseEnemy : BaseEntity
{
    private bool canSelect = true;

    private Vector3 originScale;

    void Start()
    {
        this.originScale = this.visual.transform.localScale;
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
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);   
    }

    public void OnMouseDown()
    {
        if(!canSelect) return;

        EventBus.Emit<BaseEnemy>(EnemyEventType.Select, this);
    }

    public void OnMouseEnter()
    {
        if(!canSelect) return;

        this.visual.transform.DOScale(this.originScale*1.2f, 0.2f);
    }

    public void OnMouseExit()
    {
        
        this.visual.transform.DOScale(this.originScale, 0.2f);

    }
}