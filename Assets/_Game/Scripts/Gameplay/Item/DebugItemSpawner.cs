using System.Collections.Generic;
using UnityEngine;

public class DebugItemSpawner : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ItemDatabaseSO database;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private int poolSize = 10;

    [Header("Drop Config")]
    [Tooltip("Cấu hình xác suất Star và Rarity khi debug spawn")]
    [SerializeField] private DropConfig dropConfig = new();

    [Header("Spawn Points")]
    [Tooltip("Danh sách vị trí spawn. Mỗi lần spawn sẽ dùng điểm kế tiếp (quay vòng).")]
    [SerializeField] private List<Transform> spawnPoints = new();

    private ObjectPool _pool;
    private int _nextSpawnIndex = 0;

    private void Start()
    {
        if (worldItemPrefab != null)
        {
            _pool = new ObjectPool(worldItemPrefab, poolSize);
        }
        else
        {
            Debug.LogError("DebugItemSpawner: worldItemPrefab chưa được gán!");
        }
    }

    public void SpawnRandomItem()
    {
        if (database == null)
        {
            Debug.LogError("DebugItemSpawner: database chưa được gán!");
            return;
        }

        if (_pool == null)
        {
            Debug.LogError("DebugItemSpawner: Pool chưa được tạo!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("DebugItemSpawner: Chưa có SpawnPoint nào! Kéo Transform vào danh sách spawnPoints.");
            return;
        }

        ItemDefinitionSO def = database.GetRandom();
        if (def == null) return;

        ItemInstance instance = ItemFactory.CreateRandom(def, dropConfig);

        GameObject obj = _pool.Get();
        WorldItem worldItem = obj.GetComponent<WorldItem>();

        if (worldItem == null)
        {
            Debug.LogError("DebugItemSpawner: WorldItem prefab thiếu component WorldItem!");
            return;
        }

        // Lấy spawn point theo thứ tự tuần tự, quay vòng khi hết
        Transform spawnPoint = spawnPoints[_nextSpawnIndex % spawnPoints.Count];
        _nextSpawnIndex++;

        obj.transform.position = spawnPoint.position;
        worldItem.Init(instance);

        EventBus.Emit(ItemEventType.OnItemSpawned, instance);
        Debug.Log($"🎲 Spawned: [{instance.Rarity}] {instance.DisplayName} (Star {instance.StarLevel}) tại điểm [{_nextSpawnIndex - 1}]");
    }
}
