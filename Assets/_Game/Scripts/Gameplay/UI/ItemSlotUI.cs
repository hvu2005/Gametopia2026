using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI đại diện cho một ô trong túi đồ.
/// Khi người chơi click vào slot có item → raise OnSlotClicked để InventoryUI xử lý equip.
/// </summary>
public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color emptyColor   = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    [SerializeField] private Color filledColor  = new Color(1f, 1f, 1f, 1f);

    /// <summary>Event khi slot được click — InventoryUI lắng nghe để xử lý equip.</summary>
    public event Action<ItemSlotUI> OnSlotClicked;

    /// <summary>Event khi slot được chuột PHẢI — InventoryUI lắng nghe để xử lý despawn.</summary>
    public event Action<ItemSlotUI> OnSlotRightClicked;

    public bool IsEmpty { get; private set; } = true;
    public ItemInstance CurrentItem { get; private set; }

    /// <summary>Index của slot này trong danh sách slots của InventoryUI.</summary>
    public int SlotIndex { get; private set; }

    // ─── Canvas Position ─────────────────────────────────────────────────────

    public Vector3 WorldPosition
    {
        get
        {
            if (_canvas == null) _canvas = GetComponentInParent<Canvas>();

            if (_canvas != null && _canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                return transform.position;

            RectTransform rt = transform as RectTransform;
            if (rt != null)
            {
                Camera cam = (_canvas != null ? _canvas.worldCamera : null) ?? Camera.main;
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, rt.position);
                RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, screenPos, cam, out Vector3 worldPos);
                return worldPos;
            }

            return transform.position;
        }
    }

    private Canvas _canvas;

    // ─── Setup ───────────────────────────────────────────────────────────────

    /// <summary>Gán bởi InventoryUI trong Awake để slot biết index của chính nó.</summary>
    public void SetSlotIndex(int index) => SlotIndex = index;

    // ─── Item Display ────────────────────────────────────────────────────────

    public void SetItem(ItemInstance item)
    {
        CurrentItem = item;
        IsEmpty = false;

        if (iconImage != null)
        {
            iconImage.sprite = item.Definition.Icon;
            iconImage.enabled = true;
            iconImage.color = filledColor;
        }

        if (backgroundImage != null)
            backgroundImage.color = filledColor;
    }

    public void Clear()
    {
        CurrentItem = null;
        IsEmpty = true;

        if (iconImage != null)
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
        }

        if (backgroundImage != null)
            backgroundImage.color = emptyColor;
    }

    // ─── Click Handling ──────────────────────────────────────────────────────

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsEmpty) return; // slot trống → không làm gì

        if (eventData.button == PointerEventData.InputButton.Left)
            OnSlotClicked?.Invoke(this);        // Trái: Trang bị
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnSlotRightClicked?.Invoke(this);  // Phải: Xóa/Despawn
    }

    // ─── Unity Lifecycle ─────────────────────────────────────────────────────

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        Clear();
    }
}
