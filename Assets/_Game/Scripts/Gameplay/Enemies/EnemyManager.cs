


using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager
{
    [SerializeField] private List<Transform> rectSlotList;
    public List<BaseEnemy> enemiesInBattle = new();
    public BaseEnemy enemyPrefab;
    public BaseEnemy CreateEnemy(EnemyDataSO enemyData, Transform parent, int level)
    {
        var newEnemy = Object.Instantiate(enemyPrefab, parent);
        newEnemy.Init(enemyData, level);
        var localScale = newEnemy.visual.transform.localScale;
        newEnemy.visual.transform.localScale = new Vector3(localScale.x * enemyData.Direction, localScale.y, localScale.z);
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

            var newEnemy = CreateEnemy(level.enemies[i], rectSlotList[i], level.levelID);
            enemiesInBattle.Add(newEnemy);
        }
    }

}