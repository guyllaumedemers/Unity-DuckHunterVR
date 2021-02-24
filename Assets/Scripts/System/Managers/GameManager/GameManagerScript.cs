using UnityEngine;

public class GameManagerScript : Singleton<GameManagerScript>
{
    public DuckSpawnerController duckSpawnerController;
    public GameButton gameButton;
    
    private bool isRoundLaunch;
    private bool isRunning;
    private GameMode.Mode gameMode;


    protected override void Awake()
    {
        base.Awake();
        isRunning = false;
        isRoundLaunch = false;
    }

    public GameMode.Mode CurrentMode { get => gameMode; set { gameMode = value; } }
    public bool GetRoundStatus { get => isRoundLaunch; set { isRoundLaunch = value; } }
    public bool GetGameState { get => isRunning; set { isRunning = value; } }

    public void StartDuckSpawner() => duckSpawnerController.StartSpawner(gameMode);
    public void StopDuckSpawner() => duckSpawnerController.StopSpawner();
}
