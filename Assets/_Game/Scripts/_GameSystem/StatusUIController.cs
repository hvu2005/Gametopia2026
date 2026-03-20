using UnityEngine;
using TMPro;

public class StatusUIController : MonoBehaviour
{
    [Header("Poison Status")]
    public GameObject poisonIconObj;
    public TextMeshPro poisonStackText;

    [Header("Armor Status")]
    public GameObject armorIconObj;
    public TextMeshPro armorValueText;

    [Header("Stun Status")]
    public GameObject stunIconObj;
    public TextMeshPro stunStatusText;

    public void UpdatePoisonStack(int stack)
    {
        if (stack > 0)
        {
            if (poisonIconObj) poisonIconObj.SetActive(true);
            if (poisonStackText)
            {
                poisonStackText.gameObject.SetActive(true);
                poisonStackText.text = "x" + stack;
            }
        }
        else
        {
            if (poisonIconObj) poisonIconObj.SetActive(false);
            if (poisonStackText) poisonStackText.gameObject.SetActive(false);
        }
    }

    public void UpdateArmor(float armor)
    {
        if (armor > 0)
        {
            if (armorIconObj) armorIconObj.SetActive(true);
            if (armorValueText)
            {
                armorValueText.gameObject.SetActive(true);
                armorValueText.text = armor.ToString("0.#");
            }
        }
        else
        {
            if (armorIconObj) armorIconObj.SetActive(false);
            if (armorValueText) armorValueText.gameObject.SetActive(false);
        }
    }

    public void UpdateStun(bool isStunned, string effectText = "Stun")
    {
        if (isStunned)
        {
            if (stunIconObj) stunIconObj.SetActive(true);
            if (stunStatusText)
            {
                stunStatusText.gameObject.SetActive(true);
                stunStatusText.text = effectText;
            }
        }
        else
        {
            if (stunIconObj) stunIconObj.SetActive(false);
            if (stunStatusText) stunStatusText.gameObject.SetActive(false);
        }
    }
}
