using UnityEngine;

public class SettingButtonUI : MonoBehaviour
{
    public void OnSettingClick()
    {
        var uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            uiController.Show(UIType.OptionsPopup);
        }
        else
        {
            Debug.LogWarning("⚠️ Không tìm thấy UIController trong scene để mở OptionsPopup.");
        }
    }
}
