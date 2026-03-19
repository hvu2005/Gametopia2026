using UnityEngine;
using TMPro;

public class StatusUIController : MonoBehaviour
{
    [Header("Poison Status")]
    public GameObject poisonIconObj;
    public TextMeshPro poisonStackText;

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
}
