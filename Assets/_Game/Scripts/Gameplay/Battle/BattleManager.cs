
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class BattleManager
{

    private EffectSystem _effectSystem = new();
    private StatProcessSystem _statProcessSystem = new();

    public async Task StartBattle(BaseEntity player, BaseEnemy target, List<BaseEnemy> otherEnemies)
    {
        await ExecutePlayerTurn(player, target);

        foreach (var enemy in otherEnemies)
        {
            if (enemy.IsDead)
            {
                Debug.Log($"{enemy.name} is dead and cannot take a turn.");
                continue;
            }
            await ExecuteEnemyTurn(enemy, player);
        }
    }

    public async Task ExecuteEnemyTurn(BaseEntity attacker, BaseEntity target)
    {
        attacker.SetActiveTurn(true);
        await Task.Delay(250);

        ExecuteTurn(attacker, target);
        await Task.Delay(250);
    }

    public async Task ExecutePlayerTurn(BaseEntity attacker, BaseEntity target)
    {
        attacker.SetActiveTurn(true);
        await Task.Delay(250);
        ExecuteTurn(attacker, target);
        await Task.Delay(250);
    }

    public void ExecuteTurn(BaseEntity attacker, BaseEntity target)
    {
        attacker.IsAttacked = false;

        Debug.Log($"Executing turn: {attacker.name} attacks {target.name}");

        _effectSystem.ApplyEffects(attacker);

        if (attacker.IsDead || target.IsDead || attacker.IsAttacked)
        {
            Debug.Log("Turn ended early due to death or attack interruption.");
            return;
        }

        _statProcessSystem.ProcessPreAttack(attacker, target);

        _statProcessSystem.ProcessOnAttack(attacker, target);

        _statProcessSystem.ProcessPostAttack(attacker, target);

        attacker.IsAttacked = true;

        attacker.OnUpdateStat();
        target.OnUpdateStat();

        // this.CheckWin();
    }

    // public void CheckWin()
    // {
    //     if (enemiesInBattle.TrueForAll(e => e.IsDead))
    //     {
    //         Debug.Log("Player wins!");
    //         // Xử lý khi người chơi thắng
    //     }
    // }
}