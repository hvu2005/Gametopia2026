


using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapManager
{
    [SerializeField] private Image currentBg;
    [SerializeField] private GameObject transitionPanel;
    [SerializeField] private RectTransform rect;
    [SerializeField] private RectTransform canvasRect;
    public MapManager()
    {

    }
    public void LoadLevelData(LevelSO level)
    {
        SetMapBg(level.background);
    }

    public void SetMapBg(Sprite bg)
    {
        currentBg.sprite = bg;
    }

   async Task Slide(Vector2 start, Vector2 end, float duration = 0.5f)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(start, end, time / duration);
            await Task.Yield();
        }

        rect.anchoredPosition = end;
    }

    public async Task ShowLeftTransition()
    {

        float canvasWidth = canvasRect.rect.width;

        Vector2 outsideLeft = new Vector2(-canvasWidth, 0);
        Vector2 center = Vector2.zero;

        transitionPanel.SetActive(true);

        // trượt từ trái vào
        await Slide(outsideLeft, center);

        await Task.Delay(300);

        // trượt ra phải
        await Slide(center, new Vector2(canvasWidth, 0));

        transitionPanel.SetActive(false);
    }

    // public async Task ShowRightTransition()
    // {

    //     float canvasWidth = canvasRect.rect.width;

    //     Vector2 outsideRight = new Vector2(canvasWidth, 0);
    //     Vector2 center = Vector2.zero;

    //     transitionPanel.SetActive(true);

    //     // trượt từ phải vào
    //     await Slide(outsideRight, center);

    //     await Task.Delay(300);

    //     // trượt ra trái
    //     await Slide(center, new Vector2(-canvasWidth, 0));

    //     transitionPanel.SetActive(false);
    // }

}