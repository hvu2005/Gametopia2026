using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int currentLevel;
    [SerializeField] private LevelSO[] levels;

    [SerializeField] private Canvas gameplayCanvas;

    public bool isStartBattle = false;

    [Header("Managers")]
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public BattleManager battleManager;
    public MapManager mapManager;
    public ItemManager itemManager;
    public UIManager uiManager;

    void Start()
    {
        itemManager.Init();

        LoadLevel(currentLevel);
        uiManager.SetUIStats(playerManager.player.Stats);
        itemManager.UpdateItemClasses();

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

        itemManager.On<Item>(ItemEventType.Equipe, (item) =>
        {
            itemManager.UpdateAddItemClasses(item, playerManager.player);
        });

        itemManager.On<Item>(ItemEventType.Unequipe, (item) =>
        {
            itemManager.UpdateRemoveItemClasses(item, playerManager.player);
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

        uiManager.On<Stats>(PlayerEventType.UpdateStats, uiManager.SetUIStats);
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

        playerManager.player.hiddenStats.rarityWeight+=2;

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
        if (!isStartBattle)
        {
            isStartBattle = true;
            battleManager.StartBattle(
                playerManager.player,
                enemy,
                enemyManager.GetAliveEnemies()
            );
        }


        _ = battleManager.StartPhase(
            playerManager.player,
            enemy,
            enemyManager.GetAliveEnemies()
        );
    }

    public void SpawnItemWhenWin()
    {
        isStartBattle = false;

        var spawnedItems = itemManager.GetRandomItemDataList(playerManager.player);
        uiManager.ShowItemSelection(spawnedItems);

        playerManager.ResetPlayer();
    }

    public void OnWinBattle(string message)
    {
        // _ = this.NextLevel();

        this.SpawnItemWhenWin();
    }

}