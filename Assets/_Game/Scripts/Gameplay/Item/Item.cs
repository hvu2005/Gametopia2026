using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public string itemName;
    public Stats stats;
    public Slot currentSlot;

    public Slot nextSlot;

    public Item mergeItem;

    public Image itemImage;

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
                    currentSlot = nextSlot;
                currentSlot.PlaceItem(this);
            });
    }

    public void OnUpgradeRank(Item item)
    {
        UnityEngine.Object.Instantiate(star, starsParent);
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