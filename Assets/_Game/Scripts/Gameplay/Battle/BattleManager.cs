
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public enum BattleEventType
{
    Win,
    Lose,
    SpawnFloatingText
}

[System.Serializable]
public class BattleManager : EventEmitter
{

    private EffectSystem _effectSystem = new();
    private StatProcessSystem _statProcessSystem = new();

    public async Task StartBattle(BaseEntity player, BaseEnemy target, List<BaseEnemy> enemiesInBattle)
    {
        var originPos = player.transform.localPosition;

        for (int i = 0; i < enemiesInBattle.Count; i++)
        {
            var enemy = enemiesInBattle[i];

            if (enemy.IsDead)
            {
                Debug.Log($"{enemy.name} is dead and cannot take a turn.");
                continue;
            }

            enemy.isHoving = false;
            enemy.canSelect = false;
        }

        // tiến lên
        await player.transform
            .DOLocalMoveX(originPos.x + 0.75f, 0.25f)
            .AsyncWaitForCompletion();

        // attack
        await ExecutePlayerTurn(player, target);

        // lùi về
        await player.transform
            .DOLocalMoveX(originPos.x, 0.25f)
            .AsyncWaitForCompletion();

        int total = 0;
        int completed = 0;

        // đếm enemy sống
        foreach (var e in enemiesInBattle)
        {
            if (!e.IsDead) total++;
        }

        for (int i = 0; i < enemiesInBattle.Count; i++)
        {
            var enemy = enemiesInBattle[i];

            if (enemy.IsDead) continue;

            float delay = i * 0.15f;

            DOVirtual.DelayedCall(delay, () =>
            {
                var originPosEnemy = enemy.transform.localPosition;

                Sequence seq = DOTween.Sequence();

                seq.Append(enemy.transform.DOLocalMoveX(originPosEnemy.x - 0.75f, 0.25f));

                seq.AppendCallback(() => _ = ExecuteEnemyTurn(enemy, player));

                seq.Append(enemy.transform.DOLocalMoveX(originPosEnemy.x, 0.25f));

                // 👉 Khi enemy này xong
                seq.OnComplete(() =>
                {
                    completed++;

                    if (completed >= total)
                    {
                        // tất cả enemy xong
                        foreach (var e in enemiesInBattle)
                        {
                            if (!e.IsDead)
                                e.canSelect = true;
                        }

                        CheckEnemies(enemiesInBattle);
                    }
                });
            });
        }

        CheckEnemies(enemiesInBattle);

    }

    public async Task ExecuteEnemyTurn(BaseEntity attacker, BaseEntity target)
    {
        attacker.SetActiveTurn(true);
        // await Task.Delay(250);

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

        _statProcessSystem.ProcessBeAttacked(attacker, target);

        attacker.IsAttacked = true;

        attacker.OnUpdateStat();
        target.OnUpdateStat();

        CameraShake.Instance.Shake(0.1f, 0.05f, 20);
    }

    public void CheckEnemies(List<BaseEnemy> enemiesInBattle)
    {
        if (enemiesInBattle.TrueForAll(e => e.IsDead))
        {
            Debug.Log("All enemies defeated! Player wins!");
            this.Emit<string>(BattleEventType.Win, "Player wins!");
        }
    }

    /// <summary>
    /// Gọi sau khi kết thúc toàn bộ combat để hồi giáp về đầy cho player và tất cả enemy còn sống.
    /// </summary>
    public void RestoreAllDefense(BaseEntity player, List<BaseEnemy> enemiesInBattle)
    {
        player.RestoreArmor();
        Debug.Log("[Defense] Giáp của nhân vật đã hồi về đầy sau combat.");
    }
}