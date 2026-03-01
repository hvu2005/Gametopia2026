using UnityEngine;

public class Player : BaseEntity
{
    // Chứa bộ khung trang bị của riêng Player (Issue 3)
    public BaseEquipmentFrame EquipmentFrame { get; protected set; }

    public BaseEnemy dummy; // Dummy để test

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BattleSystem.Instance.ExecuteTurn(this, dummy);

            BattleSystem.Instance.ExecuteTurn(dummy, this);
        }
    }
}