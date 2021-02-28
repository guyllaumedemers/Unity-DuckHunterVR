using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DuckSpawnerController : MonoBehaviour
{

    [Header("Size of spawn area")]
    public Vector3 spawnSize;
    [Header("Model used to instantiate")]
    public GameObject[] duckModels;

    [Header("Round Information")]
    public int nbDucksPerRound = 10;
    public float roundDelay = 10f;
    public float flightRoundIncrement = 0.15f;
    public float maxRoundIncrement = 10f;

    [Header("Wave Information")]
    public int nbDucksPerWave = 1;
    public float waveDelay = 2f;

    [Header("Debug Information")]
    public float roundNo = 1;
    public float roundTimer;
    public int ducksInRound;
    public float roundCountdown;
    public int ducksInWave;
    public float waveCountdown;
    public float timedRoundTime;
    public float timedRoundTimer;
    public GameMode.Mode gameMode;
    public bool isRunning;

    [Header("Text GameObjects")]
    public GameObject roundTimeUI;
    public GameObject timedRoundUI;
    
    private string strDuckParentGoName = "Spawned Ducks";
    private Transform _duckParent;
    private Coroutine _addDuckRoutine;
    private Coroutine _duckSpawnerRoutine;
    private Coroutine _roundCountdownRoutine;
    private Coroutine _timedRoundCountdownRoutine;
    private DisplayRoundTimeUI _displayRoundTime;
    private DisplayRoundTimeUI _displayTimedRoundTime;
    

    private void Start()
    {
        _displayRoundTime = roundTimeUI.GetComponent<DisplayRoundTimeUI>();
        _displayTimedRoundTime = timedRoundUI.GetComponent<DisplayRoundTimeUI>();

        roundTimeUI.SetActive(false);
        timedRoundUI.SetActive(false);
    }

    private void SetRegularRound() {
        nbDucksPerWave = 1;
        ducksInRound = nbDucksPerRound;
    }

    private void SetTimedRound()
    {
        roundNo = 0;
        nbDucksPerWave = 0;
        timedRoundTime = GameManager.Instance.timedRoundTime;
    }
    
    private void InitializeRoundVariables()
    {
        roundNo = 1;
        ducksInWave = 0;
        ducksInRound = 0;
        _duckSpawnerRoutine = null;
        isRunning = true;
    }

    public void StartSpawner(GameMode.Mode mode)
    {
        gameMode = mode;
        if(GameManager.Instance.clipBoard != null)
            GameManager.Instance.clipBoard.SetActive(false);
        
        InitializeRoundVariables();

        _roundCountdownRoutine = StartCoroutine(nameof(RoundCountdownRoutine));

        switch (gameMode)
        {
            case GameMode.Mode.REGULARMODE:
                SetRegularRound();
                StartCoroutine(nameof(RegularModeRoutine));
                break;
            case GameMode.Mode.TIMEDROUND:
                SetTimedRound();
                StartCoroutine(nameof(TimedModeRoutine));
                break;
            case GameMode.Mode.TARGETPRACTICE:
                SetRegularRound();
                StartCoroutine(nameof(RegularModeRoutine));
                break;
            default:
                Debug.Log("Invalid Game Mode, defaulting to regular mode");
                SetRegularRound();
                StartCoroutine(nameof(RegularModeRoutine));
                break;
        }
    }

    public void StopSpawner()
    {
        isRunning = false;

        if(_duckSpawnerRoutine != null)
            StopCoroutine(_duckSpawnerRoutine);
        
        if (_duckParent != null)
            Destroy(_duckParent.gameObject);
        
        if(GameManager.Instance.clipBoard != null)
            GameManager.Instance.clipBoard.SetActive(true);
    }

    private IEnumerator RegularModeRoutine()
    {
        while (isRunning)
        {
            if (_roundCountdownRoutine is null)
            {
                if (ducksInRound > 0)
                {
                    if (ducksInWave <= 0 && _duckSpawnerRoutine is null)
                        _duckSpawnerRoutine = StartCoroutine(nameof(SpawnDuckRoutine));
                }
                else
                {
                    roundNo++;
                    SetRegularRound();
                    _roundCountdownRoutine = StartCoroutine(nameof(RoundCountdownRoutine));
                }
            }
            yield return null;
        }
    }

    private IEnumerator TimedModeRoutine() {
        timedRoundTimer = timedRoundTime;
        
        if (timedRoundUI != null) {
            timedRoundUI.SetActive(true);
            _displayTimedRoundTime.UpdateTimedRoundText(timedRoundTimer);
        }
        
        while (isRunning) {
            if (_roundCountdownRoutine is null) {
                if(timedRoundTimer > 1f) {
                    timedRoundTimer -= Time.deltaTime;
                    
                    if (timedRoundUI != null) 
                        _displayTimedRoundTime.UpdateTimedRoundText(timedRoundTimer);

                    if (ducksInWave <= 0 && _duckSpawnerRoutine is null) {
                        nbDucksPerWave++;
                        _duckSpawnerRoutine = StartCoroutine(nameof(SpawnDuckRoutine));
                    }
                }
                else {
                    StartCoroutine(nameof(DisplayTimedRoundScoreRoutine));
                    
                    if(GameManager.Instance.gameButton != null){}
                        GameManager.Instance.gameButton.GetComponent<GameButton>().UpdateButton();
                    
                        break;
                }
            }
            yield return null;
        }
        
        if(timedRoundUI != null)
            timedRoundUI.SetActive(false);
    }

    private IEnumerator DisplayTimedRoundScoreRoutine() {
        roundTimeUI.SetActive(true);
        _displayRoundTime.TimeRoundEndText();
        yield return new WaitForSeconds(5);
        roundTimeUI.SetActive(false);
    }
    
    private IEnumerator RoundCountdownRoutine() {
        roundCountdown = roundDelay;
        roundTimeUI.SetActive(true);
        
        while (roundCountdown > 1f) {
            if (!isRunning) break;
            roundCountdown -= Time.deltaTime;
            _displayRoundTime.UpdateRoundText(roundNo, roundCountdown);
            yield return null;
        }
        
        roundTimeUI.SetActive(false);
        _roundCountdownRoutine = null;
    }

    private IEnumerator SpawnDuckRoutine() {
        waveCountdown = waveDelay;

        while (waveCountdown > 1f) {
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

    private Vector3 GetRandomSpawnPoint() {
        float posX = transform.position.x + Random.Range(-spawnSize.x / 2, spawnSize.x / 2);
        float posY = 0;
        float posZ = 0;

        if(gameMode == GameMode.Mode.TARGETPRACTICE)
        {
            posY = transform.position.y + Random.Range(-spawnSize.y / 2, spawnSize.y / 2);
            posZ = transform.position.z + Random.Range(-spawnSize.z / 4, spawnSize.z / 4);
        }
        else
        {
            posY = transform.position.y - spawnSize.y / 2;
            posZ = transform.position.z + Random.Range(-spawnSize.z / 2, spawnSize.z / 2);
        }
        
        return new Vector3(posX, posY, posZ);
    }

    private void InstantiateDuck() {
        try {
            GameObject duck;

            if (gameMode == GameMode.Mode.TARGETPRACTICE)
            {
                duck = Instantiate(duckModels[2], GetRandomSpawnPoint(), Quaternion.Euler(new Vector3(0, 90, 0)));
            }
            else
            {
                duck = Instantiate(duckModels[Random.Range(0, duckModels.Length - 1)], GetRandomSpawnPoint(), Quaternion.identity);
            }

            if (roundNo <= maxRoundIncrement)
                duck.GetComponent<IFlyingTarget>().FlightSpeed += flightRoundIncrement * roundNo;

            duck.GetComponent<IFlyingTarget>().SpawnSize = new Vector3(spawnSize.x / 2, spawnSize.y / 2, spawnSize.z / 2);
            duck.GetComponent<IFlyingTarget>().SpawnerPos = transform.position;
            duck.GetComponent<IFlyingTarget>().DiedDelegate += RemoveDuck;

            if (_duckParent == null)
                _duckParent = new GameObject(strDuckParentGoName).transform;

            duck.transform.SetParent(_duckParent);

            ducksInWave++;
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }

    private void RemoveDuck() {
        if (ducksInWave > 0)
            ducksInWave--;
        if (ducksInRound > 0)
            ducksInRound--;
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnSize);
    }
}
