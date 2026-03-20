using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager
{
    [SerializeField] private List<Transform> rectSlotList;
    [SerializeField] private EnemyListDataSO enemyDataList;
    public List<BaseEnemy> enemiesInBattle = new();
    public BaseEnemy enemyPrefab;

    public BaseEnemy CreateEnemy(EnemyDataSO enemyData, Transform parent, int level)
    {
        var newEnemy = Object.Instantiate(enemyPrefab, parent);
        newEnemy.Init(enemyData, level);
        var localScale = newEnemy.visual.transform.localScale;
        newEnemy.visual.transform.localScale =
            new Vector3(localScale.x * enemyData.Direction, localScale.y, localScale.z);
        return newEnemy;
    }

    public List<BaseEnemy> GetAliveEnemies()
    {
        return enemiesInBattle.FindAll(e => !e.IsDead);
    }

    public void LoadLevelData(LevelSO level)
    {
        enemiesInBattle.Clear();

        int enemyCount = Mathf.Min(rectSlotList.Count, level.enemies.Length);

        for (int i = 0; i < enemyCount; i++)
        {
            EnemyDataSO randomEnemyData = GetRandomEnemyByLevel(level.levelID);
            var newEnemy = CreateEnemy(randomEnemyData, rectSlotList[i], level.levelID);
            enemiesInBattle.Add(newEnemy);
        }
    }


    private EnemyDataSO GetRandomEnemyByLevel(int level)
    {
        List<EnemyDataSO> availableEnemies = new List<EnemyDataSO>();

        for (int i = 0; i < enemyDataList.Enemies.Count; i++)
        {
            if (level >= 1 && level <= 5)
            {
                if (i <= 4) 
                    availableEnemies.Add(enemyDataList.Enemies[i]);
            }
            else
            {
                availableEnemies.Add(enemyDataList.Enemies[i]);
            }
        }

        if (availableEnemies.Count == 0)
        {
            Debug.LogError("Không có enemy hợp lệ để spawn.");
            return null;
        }

        int randomIndex = Random.Range(0, availableEnemies.Count);
        return availableEnemies[randomIndex];
    }
}