


using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemPanelUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI description;
    public Image itemImage;
    public RectTransform rect;
    private Vector3 originalPos;

    private ItemDataSO currentItemData;

    private void Awake()
    {
        originalPos = rect.localPosition;
    }

    public void SetInfoFromItem(ItemDataSO itemData)
    {
        currentItemData = itemData;
        itemName.text = itemData.ItemName;
        itemImage.sprite = itemData.Sprite;
        description.text = GetDescription(itemData.Stats);
    }

    public string GetDescription(Stats stats)
    {
        StringBuilder desc = new();

        var fields = typeof(Stats).GetFields();

        foreach (var field in fields)
        {
            var value = field.GetValue(stats);

            if (value is float va && va != 0)
            {
                desc.AppendLine($"{field.Name}: +{va}\n");
            }
        }

        return desc.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOLocalMoveY(originalPos.y + 15f, 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOLocalMoveY(originalPos.y, 0.15f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus.Emit<ItemDataSO>(ItemEventType.Select, currentItemData);
    }
}