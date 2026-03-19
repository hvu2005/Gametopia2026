using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public string itemName;
    public Stats stats;
    public Slot currentSlot;

    public Slot nextSlot;

    public Item mergeItem;

    public Image itemImage;

    public Image itemFrame;

    public RectTransform rectTransform;

    public RectTransform starsParent;
    public GameObject star;
    public int rank = 1;

    public bool canSelect = true;
    

    public void Init(ItemDataSO itemData)
    {
        itemName = itemData.ItemName;
        itemImage.sprite = itemData.Sprite;
        this.stats = new Stats() + itemData.Stats;
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
                itemFrame.color = HexToColor("#FFFFFF");
                break;

            case RarityType.Rare:
                itemFrame.color = HexToColor("#0070FF");
                break;

            case RarityType.Epic:
                itemFrame.color = HexToColor("#A335EE");
                break;

            case RarityType.Legendary:
                itemFrame.color = HexToColor("#FF8000");
                break;
        }
    }

    void OnMouseDown()
    {
        rectTransform.SetAsLastSibling();
    }

    void OnMouseDrag()
    {
        if (!canSelect) return;

        var mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseUp()
    {
        
        canSelect = false;
        if (mergeItem != null)
        {
            mergeItem.OnUpgradeRank(this);
            Destroy(this.gameObject);
            
            return;
        }

        var slotToMove = nextSlot ?? currentSlot;
        rectTransform.DOMove(
                slotToMove.GetComponent<RectTransform>().position,
                0.2f
            )
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                canSelect = true;

                if (!nextSlot) return;
                if (currentSlot != null)
                {
                    currentSlot.RemoveItem();
                }

                if (nextSlot != null)
                {
                    currentSlot = nextSlot;
                }
                currentSlot.PlaceItem(this);
            });
    }

    public void OnUpgradeRank(Item item)
    {
        UnityEngine.Object.Instantiate(star, starsParent);

        EventBus.Emit<Item>(ItemEventType.Unequipe, this);

        this.stats *= 1.5f;
        
        EventBus.Emit<Item>(ItemEventType.Equipe, this);
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slot"))
        {
            Slot slot = other.GetComponent<Slot>();
            if (slot != null)
            {
                if (slot.IsEmpty())
                {
                    nextSlot = slot;
                }
                else
                {
                    if (slot.currentItem.itemName.Equals(itemName) &&
                        rank == slot.currentItem.rank &&
                        slot.currentItem != this && rank < 3
                       )
                    {
                        mergeItem = slot.currentItem;
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Slot"))
        {
            Slot slot = other.GetComponent<Slot>();
            if (slot.currentItem == mergeItem)
            {
                mergeItem = null;
            }

            nextSlot = null;
        }
    }
}