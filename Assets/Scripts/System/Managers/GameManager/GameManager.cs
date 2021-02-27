using System.Collections;
using System.Collections.Generic;
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
    public int gorePrefabPoolSize = 10;
    public List<GameObject> gorePrefabPool;    
    private Transform _gorePrefabParent;

    private void Awake()
    {
        instance = this;
        duckSpawnerController = GameObject.FindGameObjectWithTag(DUCKSPAWNER_TAG).GetComponent<DuckSpawnerController>();
        InitializeAllBooleans();

        gorePrefabPool = new List<GameObject>(new GameObject[gorePrefabPoolSize]);

        gorePrefab = Resources.Load("Package Models/Gore_Explosion/Prefabs/Gore_Explosion") as GameObject;

        _gorePrefabParent = GameObject.Find("GorePool").transform;
    }

    private void InitializeAllBooleans()
    {
        isGoreEnabled = PlayerPrefs.GetInt("EnableGore") == 1 ? true : false;
        _disableAllSound = false;
        _isRunning = false;        
    }

    private void Update()
    {
        isGoreEnabled = PlayerPrefs.GetInt("EnableGore") == 1 ? true : false;

        StartCoroutine("FillGorePool");
    }

    IEnumerator FillGorePool()
    {
        if(Time.time > 5f)
        {
            yield return new WaitForSeconds(30.0f);
        }        

        for (int i = 0; i < gorePrefabPool.Count; i++)
        {
            if (gorePrefabPool[i] == null)
            {
                gorePrefabPool[i] = Instantiate(gorePrefab, _gorePrefabParent);
            }
        }
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
