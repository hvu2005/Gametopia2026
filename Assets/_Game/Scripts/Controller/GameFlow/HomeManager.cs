using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void OnBtnStartClick()
    {
        // Chuyển sang scene Loading
        SceneManager.LoadScene("Loading");
    }

    public void OnBtnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
