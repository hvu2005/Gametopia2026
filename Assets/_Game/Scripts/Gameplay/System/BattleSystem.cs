using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class BattleSystem : Singleton<BattleSystem>
{
    public Player player;
    public List<BaseEnemy> enemiesInBattle = new List<BaseEnemy>();
    void Start()
    {
        // Đăng ký các hiệu ứng vào hệ thống
    }

    public async void StartBattle(BaseEnemy target)
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
    }

    public async Task ExecuteEnemyTurn(BaseEntity attacker, BaseEntity defender)
    {
        attacker.SetActiveTurn(true);
        await Task.Delay(250);
        foreach(var enemy in enemiesInBattle)
        {
            if(enemy != attacker)
            {
                enemy.SetActiveTurn(false);
            }
        }
        ExecuteTurn(attacker, defender);
        await Task.Delay(250);

        foreach(var enemy in enemiesInBattle)
        {
            enemy.SetActiveTurn(true);
        }
    }

    public async Task ExecutePlayerTurn(BaseEntity attacker, BaseEntity defender)
    {
        attacker.SetActiveTurn(true);
        await Task.Delay(250);
        ExecuteTurn(attacker, defender);
        await Task.Delay(250);
    }

    public void ExecuteTurn(BaseEntity attacker, BaseEntity defender)
    {
        attacker.IsAttacked = false;
        
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
            Debug.Log("Turn ended early due to death or attack interruption.");
            return;
        }

        StatSystem.Instance.ProcessPreAttack(attacker, defender);

        StatSystem.Instance.ProcessOnAttack(attacker, defender);

        StatSystem.Instance.ProcessPostAttack(attacker, defender);

        attacker.IsAttacked = true;

        attacker.OnUpdateStat();
        defender.OnUpdateStat();

        this.CheckWin();
    }

    public void CheckWin()
    {
        if (enemiesInBattle.TrueForAll(e => e.IsDead))
        {
            Debug.Log("Player wins!");
            // Xử lý khi người chơi thắng
        }
    }
}