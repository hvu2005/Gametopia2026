using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePopupUI : MonoBehaviour
{
    public void OnRestartClick()
    {
        gameObject.SetActive(false);
        EventBus.Clear();
        SceneManager.LoadScene("Gameplay");
    }

    public void OnReturnHomeClick()
    {
        gameObject.SetActive(false);
        EventBus.Clear();
        SceneManager.LoadScene("Home");
    }
}
