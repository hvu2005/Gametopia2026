using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsPopupUI : MonoBehaviour
{
    public void OnRestartClick()
    {
        gameObject.SetActive(false);
        EventBus.Clear();
        // Hiện tại Restart = chơi lại từ đầu scene Gameplay để tránh lỗi state (Level 0, mất đồ)
        SceneManager.LoadScene("Gameplay");
    }

    public void OnReturnHomeClick()
    {
        gameObject.SetActive(false);
        EventBus.Clear();
        SceneManager.LoadScene("Home");
    }

    public void OnCloseClick()
    {
        // Có thể ẩn popup này qua UIController hoặc SetActive(false) trực tiếp báo UIController
        var uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            uiController.Hide(UIType.OptionsPopup);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
