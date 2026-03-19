using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FloatingTextPrefabData
{
    public FloatingTextType Type;
    public FloatingTextController Prefab;
    
    [Header("Settings Override")]
    public bool overrideSettings;
    public float defaultOffsetX;
    public float defaultOffsetY;
    public float randomizeOffsetX;
}

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    [Header("Global Settings")]
    public float defaultOffsetX = 0f;
    public float defaultOffsetY = 1.5f;
    [Tooltip("Thêm một khoảng chênh lệch ngẫu nhiên trên trục X để các text không đè lên nhau.")]
    public float randomizeOffsetX = 0.5f;

    [Tooltip("Thời gian chờ (giây) giữa các lần spawn text liên tiếp trên cùng 1 đối tượng.")]
    public float delayBetweenSpawns = 0.2f;

    [Header("Prefab Mapping")]
    public List<FloatingTextPrefabData> prefabs;

    private Dictionary<FloatingTextType, FloatingTextController> prefabMap;
    
    // Quản lý hàng đợi spawn cho từng Entity để tránh đè chập
    private Dictionary<BaseEntity, Queue<FloatingTextEventData>> entityQueues = new();
    private HashSet<BaseEntity> activeCoroutines = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        prefabMap = new Dictionary<FloatingTextType, FloatingTextController>();
        foreach (var p in prefabs)
        {
            if (!prefabMap.ContainsKey(p.Type))
                prefabMap.Add(p.Type, p.Prefab);
        }
    }

    private void OnEnable()
    {
        EventBus.On<FloatingTextEventData>(BattleEventType.SpawnFloatingText, OnSpawnFloatingText);
    }

    private void OnDisable()
    {
        EventBus.Off<FloatingTextEventData>(BattleEventType.SpawnFloatingText, OnSpawnFloatingText);
    }

    private void OnSpawnFloatingText(FloatingTextEventData data)
    {
        if (data.Target == null) return;

        if (!entityQueues.ContainsKey(data.Target))
        {
            entityQueues[data.Target] = new Queue<FloatingTextEventData>();
        }

        entityQueues[data.Target].Enqueue(data);

        // Nếu coroutine này chưa được chạy thì bật lên
        if (!activeCoroutines.Contains(data.Target))
        {
            StartCoroutine(ProcessQueue(data.Target));
        }
    }

    private IEnumerator ProcessQueue(BaseEntity target)
    {
        activeCoroutines.Add(target);
        Queue<FloatingTextEventData> queue = entityQueues[target];

        while (queue.Count > 0)
        {
            // Stop nếu target bị huỷ giữa chừng (chết, destroy)
            if (target == null || target.gameObject == null)
            {
                queue.Clear();
                break;
            }

            FloatingTextEventData data = queue.Dequeue();
            SpawnActualText(data);

            // Bắt buộc phải chờ delay ngay cả khi queue có vẻ đang rỗng 
            // vì trong 1 frame có thể nhiều logic processor cùng ném event vào
            yield return new WaitForSeconds(delayBetweenSpawns);
        }

        activeCoroutines.Remove(target);
    }

    private void SpawnActualText(FloatingTextEventData data)
    {
        // Cần tìm prefab mapping thỏa mãn
        FloatingTextPrefabData prefabData = prefabs.Find(p => p.Type == data.Type);
        if (prefabData.Prefab == null)
        {
            Debug.LogWarning($"[FloatingTextManager] No prefab mapped for type: {data.Type}");
            return;
        }

        float offsetX = prefabData.overrideSettings ? prefabData.defaultOffsetX : defaultOffsetX;
        float offsetY = prefabData.overrideSettings ? prefabData.defaultOffsetY : defaultOffsetY;
        float randOffsetX = prefabData.overrideSettings ? prefabData.randomizeOffsetX : randomizeOffsetX;

        Vector3 spawnPos = data.Target.transform.position + new Vector3(offsetX, offsetY, 0);
        spawnPos.x += data.OffsetBuffer.x;
        spawnPos.y += data.OffsetBuffer.y;

        // Thêm khoảng random X để tránh text đè lấp lên nhau
        if (randOffsetX > 0)
        {
            spawnPos.x += UnityEngine.Random.Range(-randOffsetX, randOffsetX);
        }

        GameObject instanceObj;
        string autoPoolName = prefabData.Prefab.gameObject.name;

        if (PoolController.Instance != null)
        {
            try 
            {
                var pool = PoolController.Instance.GetPool(autoPoolName);
                instanceObj = pool.Get();
            }
            catch (KeyNotFoundException)
            {
                // Tự động khởi tạo Pool nếu chưa có trong cấu hình của PoolController
                PoolItem newItem = new PoolItem 
                { 
                    name = autoPoolName, 
                    prefab = prefabData.Prefab.gameObject, 
                    initialSize = 5 
                };
                PoolController.Instance.CreatePool(newItem);
                instanceObj = PoolController.Instance.GetPool(autoPoolName).Get();
            }
        }
        else
        {
            instanceObj = Instantiate(prefabData.Prefab.gameObject);
        }

        instanceObj.transform.position = spawnPos;
        
        FloatingTextController controller = instanceObj.GetComponent<FloatingTextController>();
        if (controller != null)
        {
            controller.poolName = autoPoolName; // Truyền tên pool qua cho controller
            controller.Init(data);
        }
    }
}
