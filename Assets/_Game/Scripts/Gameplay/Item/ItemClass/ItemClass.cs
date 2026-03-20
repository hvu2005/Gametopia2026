



using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemClass : MonoBehaviour, IPointerEnterHandler
{
    public ItemClassType itemClassType;
    public int count = 0;
    public TextMeshProUGUI countText;
    public Image icon;
    [TextArea(5,10)]
    public string description;
    public void SetOpacity(float amount)
    {
        Color c = icon.color;
        c.a = Mathf.Clamp01(amount);
        icon.color = c;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        EventBus.Emit<string>(UIEvent.HoverDesc, description);
    }
}