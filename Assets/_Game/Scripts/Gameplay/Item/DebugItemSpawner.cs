using UnityEngine;

public class DebugItemSpawner : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ItemDatabaseSO database;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private int poolSize = 10;

    [Header("Spawn Area")]
    [Tooltip("Khu vực spawn item trong viewport (0-1). Default: giữa màn hình")]
    [SerializeField] private Vector2 viewportMin = new Vector2(0.2f, 0.3f);
    [SerializeField] private Vector2 viewportMax = new Vector2(0.8f, 0.7f);

    private ObjectPool _pool;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

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

        ItemDataSO randomItem = database.GetRandom();
        if (randomItem == null) return;

        GameObject obj = _pool.Get();
        WorldItem worldItem = obj.GetComponent<WorldItem>();

        if (worldItem == null)
        {
            Debug.LogError("DebugItemSpawner: WorldItem prefab thiếu component WorldItem!");
            return;
        }

        // Vị trí ngẫu nhiên trong viewport
        float viewX = Random.Range(viewportMin.x, viewportMax.x);
        float viewY = Random.Range(viewportMin.y, viewportMax.y);
        Vector3 worldPos = _camera.ViewportToWorldPoint(new Vector3(viewX, viewY, 10f));
        worldPos.z = 0f;

        obj.transform.position = worldPos;
        worldItem.Init(randomItem);

        EventBus.Emit(ItemEventType.OnItemSpawned, randomItem);
        Debug.Log($"🎲 Spawned: {randomItem.ItemName} at ({viewX:F2}, {viewY:F2})");
    }
}
