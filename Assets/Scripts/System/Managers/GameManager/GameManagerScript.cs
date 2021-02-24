using UnityEngine;

public class GameManagerScript : Singleton<GameManagerScript>
{
    public DuckSpawnerController duckSpawnerController;
    public GameButton gameButton;
    
    private bool isRoundLaunch;
    private bool isRunning;
    private GameMode.Mode mode;


    protected override void Awake()
    {
        base.Awake();
        isRunning = false;
        isRoundLaunch = false;
    }

    public GameMode.Mode CurrentMode { get => mode; set { mode = value; } }
    public bool GetRoundStatus { get => isRoundLaunch; set { isRoundLaunch = value; } }
    public bool GetGameState { get => isRunning; set { isRunning = value; } }
}
