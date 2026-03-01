using System.Collections.Generic;
using UnityEngine;


public class BattleSystem : Singleton<BattleSystem>
{

    void Start()
    {
        // Đăng ký các hiệu ứng vào hệ thống
    }
    public void ExecuteTurn(BaseEntity attacker, BaseEntity defender)
    {
        Debug.Log($"Executing turn: {attacker.name} attacks {defender.name}");
        foreach (var effect in attacker.ActiveEffects)
        {
            if (effect is IPreAttack preAttackEffect)
            {
                preAttackEffect.ProcessPreAttack(attacker, defender);
            }
        }

        if(attacker.IsDead || defender.IsDead || attacker.IsAttacked)
        {
            return;
        }

        StatSystem.Instance.ProcessPreAttack(attacker, defender);

        StatSystem.Instance.ProcessOnAttack(attacker, defender);

        StatSystem.Instance.ProcessPostAttack(attacker, defender);
    }
}