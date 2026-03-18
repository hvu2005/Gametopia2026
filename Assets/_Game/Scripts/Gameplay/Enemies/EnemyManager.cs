


using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager
{
    [SerializeField] private List<Transform> rectSlotList;
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
        for (int i = 0; i < level.enemies.Length; i++)
        {
            var enemyPrefab = level.enemies[i];

            var newEnemy = CreateEnemy(enemyPrefab, rectSlotList[i]);
            enemiesInBattle.Add(newEnemy);
        }
    }

}