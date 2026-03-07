


using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapManager
{
    [SerializeField] private Image currentBg;
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
}