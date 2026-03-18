


using System;
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
    public Image halo;
    public RectTransform rect;
    private Vector3 originalPos;

    private ItemDataSO currentItemData;

    private void Awake()
    {
        originalPos = rect.localPosition;
    }

    void Start()
    {
        halo.transform
        .DORotate(new Vector3(0, 0, 360f), 40f, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);
    }

    public void SetInfoFromItem(ItemDataSO itemData)
    {
        currentItemData = itemData;
        itemName.text = itemData.ItemName;
        itemImage.sprite = itemData.Sprite;
        description.text = GetDescription(itemData.Stats);
        this.ChangeFrameColor(itemData.Rarity);
    }

    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        byte a = 255;
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }

        return new Color32(r, g, b, a);
    }

    public void ChangeFrameColor(RarityType rarity)
    {
        switch (rarity)
        {
            case RarityType.Normal:
                halo.color = HexToColor("#FFFFFF");
                break;

            case RarityType.Rare:
                halo.color = HexToColor("#0070FF");
                break;

            case RarityType.Epic:
                halo.color = HexToColor("#A335EE");
                break;

            case RarityType.Legendary:
                halo.color = HexToColor("#FF8000");
                break;
        }
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
                desc.AppendLine($"{field.Name}: +{va}");
            }
        }

        return desc.ToString();
    }

    public void OnDisable()
    {
        this.rect.localPosition = originalPos;
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