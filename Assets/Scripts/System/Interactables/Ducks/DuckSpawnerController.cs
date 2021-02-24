using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckSpawnerController : MonoBehaviour {

    [Header("Size of spawn area")]
    public Vector3 spawnSize;
    [Header("Model used to instantiate")]
    public GameObject[] duckModels;

    [Header("Round Information")]
    public int nbDucksPerRound = 10;
    public float roundDelay = 10f;
    public float timedRound = 60f;
    public float flightRoundIncrement = 0.15f;
    public float maxRoundIncrement = 10f;

    [Header("Wave Information")]
    public int nbDucksPerWave = 1;
    public float waveDelay = 4f;
    public float addToWaveDelay = 10f;
    
    [Header("Debug Information")]
    public float roundNo = 1;
    public float roundTimer;
    [SerializeField] private int _ducksInRound;
    public float roundCountdown;
    [SerializeField] private int _ducksInWave;
    public float waveCountdown;
    public float timedRoundTimer;
    public bool isPg13;
    [SerializeField]
    private GameMode.Mode gameMode;
    public bool isRunning;
    
    [Header("Text GameObjects")]
    public GameObject roundTimeUI;
    public GameObject timedRoundUI;
    
    private bool _timedRoundOver;
    private float _startRoundNo;
    private string strDuckParentGoName = "Spawned Ducks";
    
    private Transform _duckParent;
    private Coroutine _addDuckRoutine;
    private Coroutine _duckSpawnerRoutine;
    private DisplayRoundTimeUI _displayRoundTime;
    private DisplayRoundTimeUI _displayTimedRoundTime;
    
    public void Start() {
        _startRoundNo = roundNo;
        _displayRoundTime = roundTimeUI.GetComponent<DisplayRoundTimeUI>();
        _displayTimedRoundTime = timedRoundUI.GetComponent<DisplayRoundTimeUI>();
        
        roundTimeUI.SetActive(false);
        timedRoundUI.SetActive(false);

        //GameManagerScript.Instance.gameButton.UpdateButton();
    }
    
    private void SetRegularRound() {
        _ducksInRound = nbDucksPerRound;
        roundCountdown = roundDelay;
    }
    
    private void SetTimedRound() {
        _timedRoundOver = false;
        roundCountdown = roundDelay;
        timedRoundTimer = timedRound;
        waveDelay = 0.1f;
        roundNo = 0;
    }
    
    public void StartSpawner(GameMode.Mode mode) {
        gameMode = mode;
        _ducksInWave = 0;
        _ducksInRound = 0;
        roundNo = _startRoundNo;
        _duckSpawnerRoutine = null;
        _addDuckRoutine = null;
        
        roundTimeUI.SetActive(true);
        
        switch (gameMode) {
            
            case GameMode.Mode.REGULARMODE:
                SetRegularRound();
                break;

            case GameMode.Mode.TIMEDROUND:
                SetTimedRound();
                break;
            
            default:
                Debug.Log("Invalid Game Mode, defaulting to regular mode");
                SetRegularRound();
                break;
        }
        
        isRunning = true;
    }

    public void StopSpawner() {
        isRunning = false;
        
        if (_duckParent != null)
            Destroy(_duckParent.gameObject);
    }

    private void Update() {
        
        if (isRunning) {
            
            switch (gameMode) {
            
                case GameMode.Mode.REGULARMODE:
                    RegularModeUpdate();
                    break;

                case GameMode.Mode.TIMEDROUND:
                    if (!_timedRoundOver) {
                        TimedModeUpdate();
                    }
                    break;
            
                default:
                    RegularModeUpdate();
                    break;
            }   
        }
    }
    
    private void RegularModeUpdate() {
        
        if (roundCountdown <= 0.9f) {
            roundTimeUI.SetActive(false);
            
            if (_ducksInRound > 0) {
                if (_ducksInWave <= 0 && _duckSpawnerRoutine == null)
                    _duckSpawnerRoutine = StartCoroutine(nameof(SpawnDuckRoutine));
            }
            else {
                roundNo++;
                roundTimeUI.SetActive(true);
                SetRegularRound();
            }
        }
        else {
            roundCountdown -= Time.deltaTime;
            _displayRoundTime.UpdateRoundText(roundNo, roundCountdown);
        }
    }

    private void TimedModeUpdate() {

        if (roundCountdown <= 0.9f) {
            roundTimeUI.SetActive(false);
            timedRoundUI.SetActive(true);
            
            if (_addDuckRoutine == null)
                _addDuckRoutine = StartCoroutine(nameof(AddDuckToWaveRoutine));
            
            if (timedRoundTimer > 0) {
                if (_ducksInWave <= 0 && _duckSpawnerRoutine == null) {
                    _duckSpawnerRoutine = StartCoroutine(nameof(SpawnDuckRoutine));
                }
                timedRoundTimer -= Time.deltaTime;
                _displayTimedRoundTime.UpdateTimedRoundText(timedRoundTimer);
            }
            else {
                roundTimeUI.SetActive(true);
                timedRoundUI.SetActive(false);
                _displayRoundTime.TimeRoundEndText();
                
                GameManager.Instance.gameButton.UpdateButton();
            }
        }
        else {
            roundCountdown -= Time.deltaTime;
            _displayRoundTime.UpdateRoundText(roundNo, roundCountdown);
        }
    }

    private IEnumerator SpawnDuckRoutine() {
        waveCountdown = waveDelay;

        while (waveCountdown > 0) {
            waveCountdown -= Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < nbDucksPerWave; i++) {
            if (i > 0)
                yield return new WaitForSeconds(Random.Range(1, waveDelay));

            if (!isRunning) break;
            InstantiateDuck();
        }

        _duckSpawnerRoutine = null;
    }

    private IEnumerator AddDuckToWaveRoutine() {
        while (true) {
            if(!isRunning) break;
            yield return new WaitForSeconds(addToWaveDelay);
            nbDucksPerWave++;
        }
        _addDuckRoutine = null;
    }
    
    private Vector3 GetRandomSpawnPoint() {
        float posX = transform.position.x + Random.Range(-spawnSize.x / 2, spawnSize.x / 2);
        float posY = transform.position.y - spawnSize.y / 2;
        float posZ = transform.position.z + Random.Range(-spawnSize.z / 2, spawnSize.z / 2);

        return new Vector3(posX, posY, posZ);
    }

    private void InstantiateDuck() {
        try {
            GameObject duck = Instantiate(duckModels[Random.Range(0, duckModels.Length)], GetRandomSpawnPoint(), Quaternion.identity);

            if (roundNo <= maxRoundIncrement)
                duck.GetComponent<IFlyingTarget>().FlightSpeed += flightRoundIncrement * roundNo;
            
            duck.GetComponent<IFlyingTarget>().SpanwerPos = transform.position;
            duck.GetComponent<IFlyingTarget>().SpawnSize = new Vector3(spawnSize.x / 2, spawnSize.y / 2, spawnSize.z / 2);
            duck.GetComponent<IFlyingTarget>().DiedDelegate += RemoveDuck;

            if (_duckParent == null)
                _duckParent = new GameObject(strDuckParentGoName).transform;
            
            duck.transform.SetParent(_duckParent);
            
            _ducksInWave++;
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }

    void RemoveDuck() {
        if (_ducksInWave > 0)
            _ducksInWave--;
        if (_ducksInRound > 0)
            _ducksInRound--;
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnSize);
    }
}
