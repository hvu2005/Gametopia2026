using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int currentLevel;
    [SerializeField] private LevelSO[] levels;

    [SerializeField] private Canvas gameplayCanvas;

    public bool isStartBattle = false;

    [Header("Managers")] public PlayerManager playerManager;
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
        battleManager.On<string>(BattleEventType.Lose, OnLoseBattle);

        playerManager.On<Item>(ItemEventType.Equipe, (item) => { playerManager.EquipItem(item); });

        playerManager.On<Item>(ItemEventType.Unequipe, (item) => { playerManager.UnequipItem(item); });

        itemManager.On<Item>(ItemEventType.Equipe,
            (item) => { itemManager.UpdateAddItemClasses(item, playerManager.player); });

        itemManager.On<Item>(ItemEventType.Unequipe,
            (item) => { itemManager.UpdateRemoveItemClasses(item, playerManager.player); });

        uiManager.On<ItemDataSO>(ItemEventType.Select, (itemData) =>
        {
            uiManager.CloseItemPanel();
            itemManager.SpawnItem(itemData, this.gameplayCanvas.transform);

            _ = NextLevel();
        });

        uiManager.On<Stats>(UIEvent.HoverStat, (stats) =>
        {
            uiManager.SetUIStats(stats);
        });
        
        uiManager.On<string>(UIEvent.HoverDesc, (s) =>
        {
            uiManager.SetDescription(s);
        });
        
        uiManager.On<Item>(UIEvent.HoverItem, (item) =>
        {
            uiManager.SetItemStats(item);
        });

        uiManager.On<bool>(ItemEventType.Skip, (itemData) =>
        {
            uiManager.CloseItemPanel();
  
            _ = NextLevel();
        });

        battleManager.On<BaseEnemy>(EnemyEventType.Select, (enemy) => { StartBattle(enemy); });

        // uiManager.On<Stats>(PlayerEventType.UpdateStats, uiManager.SetUIStats);
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

        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlayBgm(GetMapBgmByLevel(levelIndex));
        }
    }

    private static string GetMapBgmByLevel(int levelIndex)
    {
        int mapIndex = (levelIndex % 3) + 1;
        return mapIndex switch
        {
            1 => AudioController.AudioKeys.BgmMap1,
            2 => AudioController.AudioKeys.BgmMap2,
            _ => AudioController.AudioKeys.BgmMap3
        };
    }

    public async Task NextLevel()
    {
        currentLevel++;
        if (currentLevel >= levels.Length)
        {
            Debug.Log("All levels completed!");
            var uiController = FindObjectOfType<UIController>();
            if (uiController != null)
            {
                uiController.Show(UIType.WinPopup);
            }
            await Task.Yield();
            return;
        }

        playerManager.player.hiddenStats.rarityWeight += 2;

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
            // if (AudioController.Instance != null)
            // {
            //     AudioController.Instance.PlayBgm(AudioController.AudioKeys.BgmCombatBoss);
            // }

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

    public void OnLoseBattle(string message)
    {
        Debug.Log("Game Over: " + message);
        isStartBattle = false;
        
        var uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            uiController.Show(UIType.LosePopup);
        }
    }

}