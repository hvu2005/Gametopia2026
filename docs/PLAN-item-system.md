# PLAN-item-system.md
## Hệ Thống Item – SO buff + Debug Spawn + Slot UI

### Mô tả
Thiết kế hệ thống item cho game Gametopia2026:
- **Data**: Item được định nghĩa bằng ScriptableObject (ItemDataSO) — sprite + chỉ số buff (Stats)
- **World Object**: Item xuất hiện trên màn hình dưới dạng Sprite (WorldItem prefab)
- **Debug Tool**: Nút bấm tạo item ngẫu nhiên trên màn hình (DebugItemSpawner)
- **Inventory & Slot UI**: Click item → bay về slot → cộng stat vào Player
- **Phase sau**: Popup thông tin khi hover, UI chỉ số player

---

## Codebase Context (đã đọc)

| File | Ghi chú |
|------|---------|
| `Stats.cs` | Struct đầy đủ với +/-/* operators. Có: hp, physicalDamage, def, crit, lifeSteal, fortune, magicDamage, poisonous, stunChance |
| `BaseItem.cs` | Abstract – có Stats, ItemID, ItemName, Tier, ItemType, EquipmentSlotType; hàm OnEquip/OnUnequip |
| `BaseInventory.cs` | Abstract – rỗng, chờ implement |
| `BaseEquipmentFrame.cs` | Abstract – Dictionary≤slot, item≥, hàm TryEquip / Unequip / GetTotalBonusStats |
| `BaseEntity.cs` | Abstract – có Stats, currentHp, ActiveEffects |
| `Player.cs` | Kế thừa BaseEntity – có ref EquipmentFrame |
| `UIController.cs` | Quản lý UI panels bằng UIType enum |
| `EventBus.cs` | Static pub/sub với enum key |
| `ObjectPool.cs` | Simple pool (prefab + Queue) |
| `StatSystem.cs` | Singleton – xử lý combat stat processors |
| `EquipmentSlotType` | Weapon, Head, Chest, Legs, Accessory |
| `ItemType` | Consumable, Weapon, Armor, Accessory |
| `SynergyType` | None, Warrior, Mage, Assassin, Beast, Machine |

---

## Proposed Changes

### Component 1 – Data Layer (ScriptableObject)

---

#### [NEW] `ItemDataSO.cs`
**Path**: `Assets/_Game/Scripts/_Config/Data/ItemDataSO.cs`

```csharp
[CreateAssetMenu(fileName = "Item_", menuName = "Game/Item Data")]
public class ItemDataSO : ScriptableObject
{
    public string ItemID;
    public string ItemName;
    public Sprite Icon;
    public ItemType Type;
    public EquipmentSlotType AllowedSlot;
    public Stats BuffStats; // Chỉ số sẽ cộng vào player khi equip
}
```

---

#### [NEW] `ItemDatabaseSO.cs`
**Path**: `Assets/_Game/Scripts/_Config/Data/ItemDatabaseSO.cs`

```csharp
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    public List<ItemDataSO> Items;
    public ItemDataSO GetRandom() => Items[Random.Range(0, Items.Count)];
}
```

---

### Component 2 – World Item (Sprite trên màn hình)

---

#### [NEW] `WorldItem.cs`
**Path**: `Assets/_Game/Scripts/Gameplay/Item/WorldItem.cs`

- MonoBehaviour gắn lên prefab item trong world
- Lưu reference đến `ItemDataSO`
- Khi được click → gọi `InventoryUI.Instance.TryPickUp(this)`
- Animate (DOTween hoặc Coroutine) di chuyển đến vị trí slot

```csharp
public class WorldItem : MonoBehaviour
{
    public ItemDataSO Data { get; private set; }
    
    public void Init(ItemDataSO data); // Set sprite + data
    public void FlyToSlot(Vector3 targetWorldPos, System.Action onComplete);
    private void OnMouseDown(); // Click → TryPickUp
}
```

> **Ghi chú**: Dùng `SpriteRenderer` + `BoxCollider2D` với `is Trigger = true`. Không cần Rigidbody (dùng Coroutine để animate).

---

#### [NEW] WorldItem Prefab
**Path**: `Assets/_Game/Prefab/WorldItem.prefab`

Cấu trúc GameObject:
```
WorldItem (GameObject)
├── SpriteRenderer
├── BoxCollider2D (isTrigger = true)
└── WorldItem.cs
```

---

### Component 3 – Player Inventory

---

#### [NEW] `PlayerInventory.cs`
**Path**: `Assets/_Game/Scripts/Gameplay/Inventory/PlayerInventory.cs`

Kế thừa `BaseInventory`. Chứa list `ItemDataSO` đang được trang bị.

```csharp
public class PlayerInventory : BaseInventory
{
    public const int MaxSlots = 6;
    public ItemDataSO[] equippedItems = new ItemDataSO[MaxSlots];

    public bool TryAddItem(ItemDataSO item, out int slotIndex);
    public void ApplyBuff(Player player);
    public void RemoveBuff(Player player, int slotIndex);
    public Stats GetTotalBuffStats();
}
```

Logic `ApplyBuff`: cộng `item.BuffStats` vào `player.Stats`.

---

#### [MODIFY] `Player.cs`
**Path**: `Assets/_Game/Scripts/Gameplay/Player/Player.cs`

Thêm reference đến `PlayerInventory`:
```csharp
public PlayerInventory Inventory { get; private set; }
```

---

### Component 4 – Debug Spawner UI

---

#### [NEW] `DebugItemSpawner.cs`
**Path**: `Assets/_Game/Scripts/Gameplay/Item/DebugItemSpawner.cs`

```csharp
public class DebugItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemDatabaseSO database;
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private Camera _camera;

    // Gắn lên Button onClick
    public void SpawnRandomItem();
    // Spawn WorldItem tại vị trí ngẫu nhiên trong Camera viewport
}
```

Dùng `ObjectPool` có sẵn để quản lý `WorldItem` prefab thay vì Instantiate/Destroy mỗi lần.

---

### Component 5 – Slot UI & Inventory UI

---

#### [NEW] `ItemSlotUI.cs`
**Path**: `Assets/_Game/Scripts/Gameplay/UI/ItemSlotUI.cs`

1 slot trong Inventory:
```csharp
public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image emptySlotImage;
    
    public bool IsEmpty { get; private set; } = true;
    public Vector3 WorldPosition => transform.position;
    
    public void SetItem(ItemDataSO item);
    public void Clear();
}
```

---

#### [NEW] `InventoryUI.cs`
**Path**: `Assets/_Game/Scripts/Gameplay/UI/InventoryUI.cs`

Singleton quản lý toàn bộ slots:
```csharp
public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] private List<ItemSlotUI> slots;
    [SerializeField] private Player player;

    public void TryPickUp(WorldItem worldItem);
    // 1. Tìm slot trống
    // 2. Gọi worldItem.FlyToSlot(slotPos, onComplete)
    // 3. onComplete: slot.SetItem(item.Data) + player.Inventory.TryAddItem + ApplyBuff
}
```

---

## Verification Plan

### Manual Testing (trong Unity Editor PlayMode)

**Prerequisite**: Tạo 2-3 `ItemDataSO` asset trong `Assets/_Game/Config/Items/`, điền Stats.

**Bước 1 – Debug Spawn**:
1. Play Scene trong Unity
2. Click nút **[Spawn Item]** trong Debug UI (Canvas/DebugSpawner)
3. **Expect**: Sprite item xuất hiện ở vị trí ngẫu nhiên trên màn hình

**Bước 2 – Pick Up & Fly To Slot**:
1. Click vào sprite item đang hiện trên màn hình
2. **Expect**: Item animate bay về slot đầu tiên còn trống trong InventoryUI
3. **Expect**: Icon item hiện lên trong slot đó

**Bước 3 – Stat Buff Applied**:
1. Mở Inspector, chọn `Player` GameObject
2. Quan sát field `Stats` trên `Player.cs` (hoặc thêm Debug.Log)
3. **Expect**: Sau khi pick up, Stats của Player tăng đúng theo `BuffStats` của item đó

**Bước 4 – Multiple Items**:
1. Spawn thêm items, pick up nhiều cái
2. **Expect**: Mỗi item fill vào slot tiếp theo
3. **Expect**: Stats cộng dồn đúng

---

## File Structure Tổng thể Sau Khi Implement

```
Assets/_Game/
├── Scripts/
│   ├── _Config/
│   │   └── Data/
│   │       ├── ItemDataSO.cs          [NEW]
│   │       └── ItemDatabaseSO.cs      [NEW]
│   ├── Gameplay/
│   │   ├── Item/
│   │   │   ├── WorldItem.cs           [NEW]
│   │   │   └── DebugItemSpawner.cs    [NEW]
│   │   ├── Inventory/
│   │   │   └── PlayerInventory.cs     [NEW]
│   │   ├── Player/
│   │   │   └── Player.cs              [MODIFY]
│   │   └── UI/
│   │       ├── ItemSlotUI.cs          [NEW]
│   │       └── InventoryUI.cs         [NEW]
├── Prefab/
│   └── WorldItem.prefab               [NEW - manual Unity setup]
└── Config/Items/
    ├── Item_Sword.asset                [NEW - SO asset, manual Unity setup]
    └── ItemDatabase.asset             [NEW - SO asset, manual Unity setup]
```

---

## Ghi chú thiết kế

1. **Không dùng EquipmentFrame ngay** – Phase này chỉ cần `PlayerInventory` đơn giản với array slots. EquipmentFrame sẽ dùng cho phase trang bị phức tạp hơn (drag & drop, synergy).
2. **Fly Animation** – Dùng Coroutine LerpPosition đơn giản, không cần DOTween.
3. **ObjectPool** – Dùng `ObjectPool` có sẵn trong `_Pattern/ObjectPool.cs` để pool WorldItem.
4. **EventBus** – Emit event `OnItemPickedUp(ItemDataSO)` dùng EventBus để các listener khác (future UI) có thể react.
