using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI slot hiển thị một item đang được trang bị (Equipment Panel).
/// - Chuột TRÁI: không làm gì (xem thông tin tooltip sau)
/// - Chuột PHẢI: raise OnSlotRightClicked → EquipmentUI sẽ unequip về túi
/// </summary>
public class EquipmentSlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("Slot Config")]
    [Tooltip("Loại slot này đại diện — phải khớp với AllowedSlot của item")]
    [SerializeField] private EquipmentSlotType slotType;

    [Header("UI References")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color emptyColor  = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    [SerializeField] private Color filledColor = new Color(1f, 1f, 1f, 1f);

    public EquipmentSlotType SlotType  => slotType;
    public ItemInstance CurrentItem    { get; private set; }
    public bool IsEmpty                => CurrentItem == null;

    /// <summary>Chuột phải vào slot đang có item → EquipmentUI xử lý unequip</summary>
    public event Action<EquipmentSlotUI> OnSlotRightClicked;

    // ─── Item Display ────────────────────────────────────────────────────────

    public void SetItem(ItemInstance item)
    {
        CurrentItem = item;

        if (iconImage != null)
        {
            iconImage.sprite  = item.Definition.Icon;
            iconImage.enabled = true;
            iconImage.color   = filledColor;
        }
        if (backgroundImage != null)
            backgroundImage.color = filledColor;
    }

    public void Clear()
    {
        CurrentItem = null;

        if (iconImage != null)
        {
            iconImage.sprite  = null;
            iconImage.enabled = false;
        }
        if (backgroundImage != null)
            backgroundImage.color = emptyColor;
    }

    // ─── Click ───────────────────────────────────────────────────────────────

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsEmpty) return;
        if (eventData.button == PointerEventData.InputButton.Right)
            OnSlotRightClicked?.Invoke(this);
    }

    private void Awake() => Clear();
}
