using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color emptyColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    [SerializeField] private Color filledColor = new Color(1f, 1f, 1f, 1f);

    public bool IsEmpty { get; private set; } = true;
    public ItemDataSO CurrentItem { get; private set; }

    public Vector3 WorldPosition
    {
        get
        {
            if (_canvas == null) _canvas = GetComponentInParent<Canvas>();

            if (_canvas != null && _canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return transform.position;
            }
            RectTransform rt = transform as RectTransform;
            if (rt != null)
            {
                Vector3 worldPos;
                RectTransformUtility.ScreenPointToWorldPoint(
                    _canvas != null ? _canvas.worldCamera : Camera.main,
                    transform.position,
                    out worldPos
                );
                return worldPos;
            }

            return transform.position;
        }
    }

    private Canvas _canvas;

    public void SetItem(ItemDataSO item)
    {
        CurrentItem = item;
        IsEmpty = false;

        if (iconImage != null)
        {
            iconImage.sprite = item.Icon;
            iconImage.enabled = true;
            iconImage.color = filledColor;
        }

        if (backgroundImage != null)
        {
            backgroundImage.color = filledColor;
        }
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
        {
            backgroundImage.color = emptyColor;
        }
    }

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        Clear();
    }
}
