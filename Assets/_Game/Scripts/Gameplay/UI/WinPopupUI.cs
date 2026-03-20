using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPopupUI : MonoBehaviour
{
    public void OnRestartClick()
    {
        gameObject.SetActive(false);
        EventBus.Clear();
        // Load lại scene Gameplay
        SceneManager.LoadScene("Gameplay");
    }

    public void OnReturnHomeClick()
    {
        gameObject.SetActive(false);
        EventBus.Clear();
        // Load lại scene Home
        SceneManager.LoadScene("Home");
    }
}
