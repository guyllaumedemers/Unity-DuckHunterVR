using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DuckSpawnerController : MonoBehaviour {

    [Header("Size of spawn area")]
    public Vector3 spawnSize;
    [Header("Model used to instantiate")]
    public GameObject[] duckModels;

    [Header("Round Information")]
    public int nbDucksPerRound = 10;
    public float roundDelay = 10f;
    public float timedRoundTime = 60f;
    public float roundTimer;
    public bool isTimedRoundOver;
    public float flightRoundIncrement = 0.15f;
    public float maxRoundIncrement = 10;

    [Header("Wave Information")]
    public int nbDucksPerWave = 1;
    public float waveDelay = 4f;

    [Header("Debug Information")]
    public float roundNo = 1;
    [SerializeField] private int _ducksInRound = 0;
    public float roundCountdown;

    [SerializeField] private int _ducksInWave = 0;
    public float waveCountdown;

    [HideInInspector]public bool isPg13;
    
    private bool _isSpawnRoutineRunning;
    private float _startRoundNo;
    private Transform _duckParent;
    private string strDuckParentGoName = "Spawned Ducks";

    
    private void Initialize() {
        roundNo = _startRoundNo;
        _ducksInWave = 0;
        _ducksInRound = 0;
        _ducksInRound = nbDucksPerRound;
        roundCountdown = roundDelay;
        waveCountdown = waveDelay;
        _isSpawnRoutineRunning = false;
    }
    
    private void Awake() { 
        _startRoundNo = roundNo;
        DisplayRoundTimeUI.Instance.duckSpawner = this;
    }
    
    private void OnEnable() {
        Initialize();
        DisplayRoundTimeUI.Instance.GetTimeDisplayObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (_duckParent != null)
            Destroy(_duckParent.gameObject);
        
        DisplayRoundTimeUI.Instance.GetTimeDisplayObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_duckParent != null)
            Destroy(_duckParent.gameObject);
    }

    private void Start()
    {
        switch (GameManagerScript.Instance.GetCurrentMode)
        {
            case GameManagerScript.GameMode.REGULAR_MODE:
                SetRegularRound();
                break;

            case GameManagerScript.GameMode.TIMED_MODE:
                SetTimedRound();
                break;
            
            default:
                Debug.Log("Invalid Game Mode, defaulting to regular mode");
                SetRegularRound();
                break;
        }
    }

    private void SetTimedRound() {
        roundCountdown = roundDelay;
        roundTimer = timedRoundTime;
        isTimedRoundOver = false;
    }

    private void SetRegularRound() {
        _ducksInRound = nbDucksPerRound;
        roundCountdown = roundDelay;
    }

    private void Update() {
        
        switch (GameManagerScript.Instance.GetCurrentMode) {
            case GameManagerScript.GameMode.REGULAR_MODE:
                RegularModeUpdate();
                break;

            case GameManagerScript.GameMode.TIMED_MODE:
                TimedModeUpdate();
                break;
            
            default:
                RegularModeUpdate();
                break;
        }
    }
    
    private void RegularModeUpdate()
    {
        if (roundCountdown <= 0) {
            if (_ducksInRound > 0) {
                if (_ducksInWave <= 0 && !_isSpawnRoutineRunning)
                    StartCoroutine(nameof(SpawnDuckRoutine));
            }
            else {
                roundNo++;
                DisplayRoundTimeUI.Instance.GetTimeDisplayObject.SetActive(true);
                SetRegularRound();
            }
        }
        else {
            roundCountdown -= Time.deltaTime;
        }
    }

    private void TimedModeUpdate() {
        if (roundCountdown <= 0) {
            if (roundTimer > 0) {
                if (_ducksInWave <= 0 && !_isSpawnRoutineRunning) {
                    StartCoroutine(nameof(SpawnDuckRoutine));
                    nbDucksPerWave++;
                }
                roundTimer -= Time.deltaTime;
            }
            else {
                isTimedRoundOver = true;
                DisplayRoundTimeUI.Instance.GetTimeDisplayObject.SetActive(true);
            }
        }
        else {
            roundCountdown -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnDuckRoutine()
    {
        _isSpawnRoutineRunning = true;
        waveCountdown = waveDelay;

        while (waveCountdown > 0)
        {
            waveCountdown -= Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < nbDucksPerWave; i++)
        {
            if (i > 0)
                yield return new WaitForSeconds(Random.Range(1, waveDelay));
            
            InstantiateDuck();
        }

        _isSpawnRoutineRunning = false;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float posX = transform.position.x + Random.Range(-spawnSize.x / 2, spawnSize.x / 2);
        float posY = transform.position.y - spawnSize.y / 2;
        float posZ = transform.position.z + Random.Range(-spawnSize.z / 2, spawnSize.z / 2);

        return new Vector3(posX, posY, posZ);
    }

    private void InstantiateDuck()
    {
        try
        {
            GameObject duck = Instantiate(duckModels[Random.Range(0, duckModels.Length)], GetRandomSpawnPoint(), Quaternion.identity);

            if (roundNo <= maxRoundIncrement)
                duck.GetComponent<IFlyingTarget>().FlightSpeed += flightRoundIncrement * roundNo;
            
            duck.GetComponent<IFlyingTarget>().SpanwerPos = transform.position;
            duck.GetComponent<IFlyingTarget>().SpawnSize = new Vector3(spawnSize.x / 2, spawnSize.y / 2, spawnSize.z / 2);
            duck.GetComponent<IFlyingTarget>().DiedDelegate += RemoveDuck;

            if (_duckParent == null)
                _duckParent = new GameObject(strDuckParentGoName).transform;
            
            duck.transform.SetParent(_duckParent);
            duck.GetComponent<DuckController>().isPg13 = isPg13;
            
            _ducksInWave++;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    void RemoveDuck()
    {
        if (_ducksInWave > 0)
            _ducksInWave--;
        if (_ducksInRound > 0)
            _ducksInRound--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnSize);
    }
}
