using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private bool isRunning;
    public enum GameMode
    {
        REGULAR_MODE,
        TIMED_ROUND,
        CHALLENGE_MODE
    }
    #region Singleton
    private static GameManagerScript instance;
    private GameManagerScript() { }
    public static GameManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManagerScript();
            }
            return instance;
        }
    }
    #endregion
    public GameMode mode;
    private bool isRoundLaunch;

    public void Awake()
    {
        instance = this;
        isRunning = true;
        mode = GameMode.REGULAR_MODE;
        isRoundLaunch = false;
    }

    public GameMode GetCurrentMode { get => mode; set { mode = value; } }
    public bool GetRoundStatus { get => isRoundLaunch; set { isRoundLaunch = value; } }
    public bool GetGameState { get => isRunning; set { isRunning = value; } }
}
