



using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : BaseEntity
{

    void Start()
    {

    }

    public override void OnUpdateStat()
    {
        base.OnUpdateStat();
        
        if(currentHp <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        Debug.Log($"{name} has died.");
        this.gameObject.SetActive(false);
    }

}