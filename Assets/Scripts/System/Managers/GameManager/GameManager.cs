using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    [Header("Required Components")]
    public DuckSpawnerController duckSpawnerController;
    public GameObject gameButton;
    public GameObject timeDial;
    private readonly string DUCKSPAWNER_TAG = "DuckSpawner";

    [Header("Required Informations")]
    [SerializeField] private bool _disableAllSound;
    [SerializeField] private bool _isRunning;
    [SerializeField] private GameMode.Mode _gameMode;
    
    [Header("Gore Options")]
    public bool isGoreEnabled;
    public GameObject gorePrefab;

    private void Awake()
    {
        instance = this;
        if(duckSpawnerController is null)
            duckSpawnerController = GameObject.FindGameObjectWithTag(DUCKSPAWNER_TAG).GetComponent<DuckSpawnerController>();
        
        InitializeAllBooleans();
    }

    private void InitializeAllBooleans()
    {
        isGoreEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("EnableGore"));
        
        _disableAllSound = false;
        _isRunning = false;

        gorePrefab = Resources.Load("Package Models/Gore_Explosion/Prefabs/Gore_Explosion") as GameObject;
    }

    private void Update()
    {
        isGoreEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("EnableGore"));
    }

    public GameMode.Mode CurrentMode { get => _gameMode; set { _gameMode = value; } }
    public bool GetGameState { get => _isRunning; set { _isRunning = value; } }
    /// <summary>
    /// False it is not disable, True it is disable
    /// </summary>
    public bool GetToggleDisableForSound { get => _disableAllSound; set { _disableAllSound = value; } }

    public void StartDuckSpawner()
    {
        if (duckSpawnerController != null)
        {
            duckSpawnerController.StartSpawner(_gameMode);
        }
    }

    public void StopDuckSpawner()
    {
        if (duckSpawnerController != null)
        {
            duckSpawnerController.StopSpawner();
        }
    }
}
