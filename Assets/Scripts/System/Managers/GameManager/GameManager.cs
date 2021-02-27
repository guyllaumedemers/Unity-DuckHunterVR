using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : Singleton<GameManager>
{
    public DuckSpawnerController duckSpawnerController;
    public GameButton gameButton;
    public GameObject timeDial;
    
    [Header("Gore Options")]
    public bool isGoreEnabled;
    public GameObject gorePrefab;
    private bool _disableAllSound;
    private bool _isRunning;
    private GameMode.Mode _gameMode;

    protected override void Awake()
    {
        base.Awake();
        InitializeAllBooleans();
    }

    private void InitializeAllBooleans()
    {
        isGoreEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("EnableGore"));
        
        _disableAllSound = false;
        _isRunning = false;

        gorePrefab = Resources.Load("Package Models/Gore_Explosion/Prefabs/Gore_Explosion") as GameObject;
    }

    public GameMode.Mode CurrentMode { get => _gameMode; set { _gameMode = value; } }
    public bool GetGameState { get => _isRunning; set { _isRunning = value; } }
    /// <summary>
    /// False it is not disable, True it is disable
    /// </summary>
    public bool GetToggleDisableForSound { get => _disableAllSound; set { _disableAllSound = value; } }
    public bool GetToggleEnableForGore { get => isGoreEnabled; set { isGoreEnabled = value; } }

    public void StartDuckSpawner() => duckSpawnerController.StartSpawner(_gameMode);
    public void StopDuckSpawner() => duckSpawnerController.StopSpawner();
}
