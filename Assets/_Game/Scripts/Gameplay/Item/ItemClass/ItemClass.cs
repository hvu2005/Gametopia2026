



using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemClass : MonoBehaviour
{
    public ItemClassType itemClassType;
    public int count = 0;
    public TextMeshProUGUI countText;
    public Image icon;

    public void SetOpacity(float amount)
    {
        Color c = icon.color;
        c.a = Mathf.Clamp01(amount);
        icon.color = c;
    }
}