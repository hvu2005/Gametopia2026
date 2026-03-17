




using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public Stats stats;
    public Slot currentSlot;

    public Slot nextSlot;

    public Image itemImage;

    public RectTransform rectTransform;

    public bool canSelect = true;

    public void Init(ItemDataSO itemData)
    {
        itemImage.sprite = itemData.Sprite;
        this.stats = new Stats() + itemData.Stats;
    }

    void OnMouseDown()
    {

    }

    void OnMouseDrag()
    {
        if (!canSelect) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseUp()
    {
        canSelect = false;

        rectTransform.DOMove(
            nextSlot.GetComponent<RectTransform>().position,
            0.2f
        )
        .SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            canSelect = true;
            if (currentSlot != null)
            {
                currentSlot.RemoveItem();
            }
            currentSlot = nextSlot;
            currentSlot.PlaceItem(this);
        });
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slot"))
        {
            Slot slot = other.GetComponent<Slot>();
            if (slot != null && slot.IsEmpty())
            {
                nextSlot = slot;
            }
        }
    }
}