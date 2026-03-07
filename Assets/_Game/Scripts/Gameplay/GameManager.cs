using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int currentLevel;
    [SerializeField] private LevelSO[] levels;

    [Header("Managers")]
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public BattleManager battleManager;
    public MapManager mapManager;

    void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length)
        {
            Debug.LogError("Invalid level index!");
            return;
        }

        var levelData = levels[levelIndex];
        
        mapManager.LoadLevelData(levelData);
        enemyManager.LoadLevelData(levelData);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var aliveEnemies = enemyManager.GetAliveEnemies();
            if (aliveEnemies.Count > 0)
            {
                StartBattle(aliveEnemies[0]);
            }
        }
    }

    public void StartBattle(BaseEnemy enemy)
    {
        _ = battleManager.StartBattle(
            playerManager.player,
            enemy,
            enemyManager.GetAliveEnemies()
        );
    }
}