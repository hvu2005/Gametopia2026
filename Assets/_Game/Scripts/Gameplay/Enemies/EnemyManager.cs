


using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager
{
    [SerializeField] private Transform enemyContainer;
    public List<BaseEnemy> enemiesInBattle = new();
    public BaseEnemy CreateEnemy(BaseEnemy enemyPrefab, Transform parent)
    {
        var newEnemy = Object.Instantiate(enemyPrefab, parent);
        return newEnemy;
    }

    public List<BaseEnemy> GetAliveEnemies()
    {
        return enemiesInBattle.FindAll(e => !e.IsDead);
    }

    public void LoadLevelData(LevelSO level)
    {
        enemiesInBattle.Clear();
        foreach (var enemyPrefab in level.enemies)
        {
            var newEnemy = CreateEnemy(enemyPrefab, enemyContainer);
            enemiesInBattle.Add(newEnemy);
        }
    }

}