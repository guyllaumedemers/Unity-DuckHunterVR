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

    [Header("DuckSpawner Tag")]
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
        duckSpawnerController = GameObject.FindGameObjectWithTag(DUCKSPAWNER_TAG).GetComponent<DuckSpawnerController>();
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
