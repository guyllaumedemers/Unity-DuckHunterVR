using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : Singleton<GameManager>
{
    [Header("Required Components")]
    public DuckSpawnerController duckSpawnerController;

    [Header("DuckSpawner Tag")]
    private readonly string DUCKSPAWNER_TAG = "DuckSpawner";

    [Header("Required Informations")]
    [SerializeField] private bool _disableAllSound;
    [SerializeField] private bool _isRunning;
    [SerializeField] private GameMode.Mode _gameMode;

    [Header("Gore Options")]
    public bool isGoreEnabled;
    public GameObject gorePrefab;
    /// <summary>
    /// DuckSpawnerController must be first in order to be able to launch the game when coming from the Menu scene
    /// However, it is producing a msg error inside the console since the bas.Awake() of the gamemanager is called AFTER
    /// </summary>
    protected override void Awake()
    {
        /*IMPORTANT DO NOT CHANGE THE ORDER*/
        duckSpawnerController = GameObject.FindGameObjectWithTag(DUCKSPAWNER_TAG).GetComponent<DuckSpawnerController>();
        base.Awake();
        InitializeAllBooleans();
    }

    private void InitializeAllBooleans()
    {
        isGoreEnabled = PlayerPrefs.GetInt("EnableGore") == 1 ? true : false;
        _disableAllSound = false;
        _isRunning = false;

        gorePrefab = Resources.Load("Package Models/Gore_Explosion/Prefabs/Gore_Explosion") as GameObject;
    }

    private void Update()
    {
        isGoreEnabled = PlayerPrefs.GetInt("EnableGore") == 1 ? true : false;
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
