using UnityEngine;
using System.Collections;

public enum GameState
{
    MainMenu,
    Start,
    Playing,
    Paused,
    WinLevel,
    LoseLevel,
    GameOver
}

public class GameStateManager : EventTarget
{
    
}