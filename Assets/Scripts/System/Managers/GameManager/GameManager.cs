using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : Singleton<GameManager>
{
    public DuckSpawnerController duckSpawnerController;
    public GameButton gameButton;

    public bool isGoreEnabled;
    private bool _disableAllSound;
    private bool _isRoundLaunch;
    private bool _isRunning;
    private GameMode.Mode _gameMode;

    protected override void Awake()
    {
        base.Awake();
        InitializeAllBooleans();
    }

    private void InitializeAllBooleans()
    {
        isGoreEnabled = true;
        _disableAllSound = false;
        _isRunning = false;
        _isRoundLaunch = false;
    }

    public GameMode.Mode CurrentMode { get => _gameMode; set { _gameMode = value; } }
    public bool GetRoundStatus { get => _isRoundLaunch; set { _isRoundLaunch = value; } }
    public bool GetGameState { get => _isRunning; set { _isRunning = value; } }
    /// <summary>
    /// False it is not disable, True it is disable
    /// </summary>
    public bool GetToggleDisableForSound { get => _disableAllSound; set { _disableAllSound = value; } }
    public bool GetToggleEnableForGore { get => isGoreEnabled; set { isGoreEnabled = value; } }

    public void StartDuckSpawner() => duckSpawnerController.StartSpawner(_gameMode);
    public void StopDuckSpawner() => duckSpawnerController.StopSpawner();
}
