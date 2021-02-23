using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [System.Serializable]
    public enum GameMode
    {
        REGULAR_MODE,
        TIMED_MODE,
        CHALLENGE_MODE
    }
    
    public DuckSpawnerController duckSpawnerController;
    public GameButton gameButton;
    
    
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

    private bool isRoundLaunch;
    private bool isRunning;
    private GameMode mode;
    
    
    public void Awake()
    {
        instance = this;
        //mode = GameMode.REGULAR_MODE;
        isRunning = false;
        isRoundLaunch = false;
    }

    public GameMode GetCurrentMode { get => mode; set { mode = value; } }
    public bool GetRoundStatus { get => isRoundLaunch; set { isRoundLaunch = value; } }
    public bool GetGameState { get => isRunning; set { isRunning = value; } }
}
