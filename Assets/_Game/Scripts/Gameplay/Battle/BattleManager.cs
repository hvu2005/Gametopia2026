
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public enum BattleEventType
{
    Win,
    Lose,
    SpawnFloatingText,
    Start,
    End

}

[System.Serializable]
public class BattleManager : EventEmitter
{

    [Range(1f, 4f)] public int BattleSpeed = 1;
    private EffectSystem _effectSystem = new();
    private StatProcessSystem _statProcessSystem = new();


    float t(float duration) => duration / (float)BattleSpeed;

    public void StartBattle(BaseEntity player, BaseEnemy target, List<BaseEnemy> enemiesInBattle)
    {
        player.currentArmor = player.Stats.armor;
        foreach (var enemy in enemiesInBattle)
        {
            enemy.currentArmor = enemy.Stats.armor;
        }
        EventBus.Emit<bool>(BattleEventType.Start, true);
    }

    public async Task StartPhase(BaseEntity player, BaseEnemy target, List<BaseEnemy> enemiesInBattle)
    {
        // var originPos = player.transform.localPosition;
        player.hasExtraAttacked = false;
        enemiesInBattle.ForEach(enemy =>
        {
            enemy.hasExtraAttacked = false;
        });

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

        // // tiến lên
        // await player.transform
        //     .DOLocalMoveX(originPos.x + 0.75f, t(0.25f))
        //     .AsyncWaitForCompletion();

        // // attack
        await ExecutePlayerTurn(player, target);

        // await Task.Delay((int)t(250));

        // // lùi về
        // await player.transform
        //     .DOLocalMoveX(originPos.x, t(0.25f))
        //     .AsyncWaitForCompletion();

        List<Task> enemyTasks = new List<Task>();

        for (int i = 0; i < enemiesInBattle.Count; i++)
        {
            var enemy = enemiesInBattle[i];

            if (enemy.IsDead) continue;

            float delay = i * t(0.15f);

            enemyTasks.Add(ExecuteEnemyTurn(enemy, player, (int)(delay * 1000)));
        }

        // 👉 chờ tất cả enemy đánh xong
        await Task.WhenAll(enemyTasks);

        // 👉 giờ mới check
        foreach (var e in enemiesInBattle)
        {
            if (!e.IsDead)
                e.canSelect = true;
        }

        CheckEnemies(enemiesInBattle);

    }

    public async Task ExecuteEnemyTurn(BaseEntity attacker, BaseEntity target, int delay = 0)
    {
        await Task.Delay(delay);

        // this.CreateSwordAnimation(attacker, target, 1);

        attacker.SetActiveTurn(true);
        // await Task.Delay(250);
        await ExecuteTurn(attacker, target, -1);
        // await Task.Delay((int)(250 / BattleSpeed));
    }

    public async Task ExecutePlayerTurn(BaseEntity attacker, BaseEntity target)
    {

        // this.CreateSwordAnimation(attacker, target, -1);

        attacker.SetActiveTurn(true);
        // await Task.Delay((int)(250 / BattleSpeed));

        await ExecuteTurn(attacker, target, 1);
        // await Task.Delay((int)(250 / BattleSpeed));
    }

    public async Task CreateSwordAnimation(BaseEntity attacker, BaseEntity target, int direction = 1)
    {
        var pool = PoolController.Instance.GetPool("Sword");
        var sword = pool.Get();

        Vector3 startPos = target.transform.position + new Vector3(direction * 2f, 0f, 0f);
        Vector3 endPos = target.transform.position + new Vector3(direction * 0.3f, 0f, 0f);

        sword.transform.position = startPos;
        sword.transform.localScale = new Vector3(1f, direction, 1f);

        DOTween.Sequence()
            .Append(
                sword.transform.DOMove(endPos, t(0.25f))
                    .SetEase(Ease.InExpo)
            )
            .OnComplete(() =>
            {
                pool.Release(sword);
            });
        await Task.Delay((int)t(250));
    }

    public async Task ExecuteTurn(BaseEntity attacker, BaseEntity target, int direction = 1)
    {
        attacker.IsAttacked = false;


        _effectSystem.ApplyPreEffects(attacker);

        attacker.OnUpdateStat();
        target.OnUpdateStat();

        if (attacker.IsDead || target.IsDead)
        {
            Debug.Log("Turn ended early due to death or attack interruption.");
            return;
        }

        if (!attacker.IsAttacked)
        {
            var originPos = attacker.transform.localPosition;

            while (!attacker.IsAttacked & !target.IsDead)
            {

                attacker.IsAttacked = true;

                await attacker.transform
                .DOLocalMoveX(originPos.x + 0.75f * direction, t(0.25f))
                .AsyncWaitForCompletion();

                await CreateSwordAnimation(attacker, target, -direction);


                _statProcessSystem.ProcessPreAttack(attacker, target);
                _statProcessSystem.ProcessOnAttack(attacker, target);
                _statProcessSystem.ProcessPostAttack(attacker, target);

                attacker.OnUpdateStat();
                target.OnUpdateStat();

                target.OnTakeDamage();
                CameraShake.Instance.Shake(t(0.1f), 0.05f, 20);

            }
            _statProcessSystem.ProcessBeAttacked(attacker, target);

            await attacker.transform
                .DOLocalMoveX(originPos.x, t(0.25f))
                .AsyncWaitForCompletion();

        }

        _effectSystem.ApplyPostEffects(attacker);
        _effectSystem.TryRemoveEffects(attacker);


        attacker.OnUpdateStat();
        target.OnUpdateStat();


    }

    public void CheckEnemies(List<BaseEnemy> enemiesInBattle)
    {
        if (enemiesInBattle.TrueForAll(e => e.IsDead))
        {
            Debug.Log("All enemies defeated! Player wins!");
            EventBus.Emit<bool>(BattleEventType.End, true);

            this.Emit<string>(BattleEventType.Win, "Player wins!");
        }
    }
}