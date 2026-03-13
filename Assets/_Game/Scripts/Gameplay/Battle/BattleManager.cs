
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum BattleEventType
{
    Win,
    Lose,
}

[System.Serializable]
public class BattleManager : EventEmitter
{

    private EffectSystem _effectSystem = new();
    private StatProcessSystem _statProcessSystem = new();

    public async Task StartBattle(BaseEntity player, BaseEnemy target, List<BaseEnemy> enemiesInBattle)
    {
        await ExecutePlayerTurn(player, target);

        foreach (var enemy in enemiesInBattle)
        {
            if (enemy.IsDead)
            {
                Debug.Log($"{enemy.name} is dead and cannot take a turn.");
                continue;
            }
            await ExecuteEnemyTurn(enemy, player);
        }

        this.CheckEnemies(enemiesInBattle);
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


    }

    public void CheckEnemies(List<BaseEnemy> enemiesInBattle)
    {
        if (enemiesInBattle.TrueForAll(e => e.IsDead))
        {
            this.Emit<string>(BattleEventType.Win, "Player wins!");
        }
    }

    /// <summary>
    /// Gọi sau khi kết thúc toàn bộ combat để hồi giáp về đầy cho player và tất cả enemy còn sống.
    /// </summary>
    public void RestoreAllDefense(BaseEntity player, List<BaseEnemy> enemiesInBattle)
    {
        player.RestoreDefense();
        Debug.Log("[Defense] Giáp của nhân vật đã hồi về đầy sau combat.");
    }
}