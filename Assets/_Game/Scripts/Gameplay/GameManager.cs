using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int currentLevel;
    [SerializeField] private LevelSO[] levels;

    [SerializeField] private Canvas gameplayCanvas;

    [Header("Managers")]
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public BattleManager battleManager;
    public MapManager mapManager;
    public ItemManager itemManager;
    public UIManager uiManager;

    void Start()
    {
        LoadLevel(currentLevel);

        this.RegistEvents();
    }

    public void RegistEvents()
    {
        battleManager.On<string>(BattleEventType.Win, OnWinBattle);

        playerManager.On<Item>(ItemEventType.Equipe, (item) =>
        {
            playerManager.EquipItem(item);
        });

        playerManager.On<Item>(ItemEventType.Unequipe, (item) =>
        {
            playerManager.UnequipItem(item);
        });

        uiManager.On<ItemDataSO>(ItemEventType.Select, (itemData) =>
        {
            uiManager.CloseItemPanel();
            itemManager.SpawnItem(itemData, this.gameplayCanvas.transform); 

            _ = NextLevel();
        });

        battleManager.On<BaseEnemy>(EnemyEventType.Select, (enemy) =>
        {
           StartBattle(enemy); 
        });
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

    public async Task NextLevel()
    {
        currentLevel++;
        if (currentLevel >= levels.Length)
        {
            Debug.Log("All levels completed!");
            await Task.Yield();
            return;
        }


        _ = mapManager.ShowLeftTransition();
        await Task.Delay(500);

        LoadLevel(currentLevel);
    }

    // public void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         var aliveEnemies = enemyManager.GetAliveEnemies();
    //         if (aliveEnemies.Count > 0)
    //         {
    //             StartBattle(aliveEnemies[0]);
    //         }
    //     }
    // }

    public void StartBattle(BaseEnemy enemy)
    {
        _ = battleManager.StartBattle(
            playerManager.player,
            enemy,
            enemyManager.GetAliveEnemies()
        );
    }

    public void SpawnItemWhenWin()
    {
        var spawnedItems = itemManager.GetRandomItemDataList();
        uiManager.ShowItemSelection(spawnedItems);
    }

    public void OnWinBattle(string message)
    {
        // _ = this.NextLevel();

        this.SpawnItemWhenWin();
    }

}