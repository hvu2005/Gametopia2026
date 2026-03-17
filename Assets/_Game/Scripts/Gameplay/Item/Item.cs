




using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public Stats stats;
    public Slot currentSlot;

    public Slot nextSlot;

    public RectTransform rectTransform;

    public bool canSelect = true;

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