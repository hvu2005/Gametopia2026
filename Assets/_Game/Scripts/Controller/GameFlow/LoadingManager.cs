using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private string nextSceneName = "Gameplay";
    [SerializeField] private float minLoadingTime = 1f; // Thời gian loading tối thiểu để xem được UI

    private void Start()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        // Đảm bảo progress bar bắt đầu từ 0
        if (progressBar != null)
        {
            progressBar.value = 0f;
        }

        float elapsedTime = 0f;

        // Bắt đầu load scene bất đồng bộ
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        
        // Không cho phép chuyển scene ngay lập tức khi load xong (chờ đủ thời gian tối thiểu)
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            elapsedTime += Time.deltaTime;
            
            // Progress của asyncLoad chạy từ 0 đến 0.9 là xong phần load
            float targetProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
            // Tính toán progress kết hợp với thời gian tối thiểu
            float timeProgress = Mathf.Clamp01(elapsedTime / minLoadingTime);
            
            float finalProgress = Mathf.Min(targetProgress, timeProgress);

            if (progressBar != null)
            {
                progressBar.value = finalProgress;
            }

            // Nếu đã load xong và đủ thời gian thì cho phép chuyển scene
            if (asyncLoad.progress >= 0.9f && elapsedTime >= minLoadingTime)
            {
                if (progressBar != null)
                {
                    progressBar.value = 1f;
                }
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
