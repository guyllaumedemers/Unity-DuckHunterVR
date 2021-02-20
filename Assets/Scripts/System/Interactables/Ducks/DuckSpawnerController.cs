using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckSpawnerController : MonoBehaviour {
    
    [Header("Size of spawn area")]
    public Vector3 spawnSize;
    [Header("Model used to instantiate")]
    public GameObject[] duckModels;
    [Header("Duck Parent Transform")] 
    public Transform duckParent;

    [Header("Round Information")]
    public int nbDucksPerRound = 10;
    public float roundDelay = 10f;
    public float flightRoundIncrement = 0.15f;
    
    [Header("Wave Information")]
    public int nbDucksPerWave = 1;
    public float waveDelay = 4f;
    
    [Header("Debug Information")]
    public float roundNo = 1;
    [SerializeField]private int _ducksInRound = 0;
    public float roundCountdown;
    
    [SerializeField]private int _ducksInWave = 0;
    public float waveCountdown;
    
    private bool _isSpawnRoutineRunning;
    private float p_roundNo;
    
    private void Awake() {
        p_roundNo = roundNo;
    }

    private void OnEnable() {
        roundNo = p_roundNo;
        _ducksInWave = 0;
        _ducksInRound = 0;
        _ducksInRound = nbDucksPerRound;
        roundCountdown = roundDelay;
        waveCountdown = waveDelay;
        duckParent = new GameObject("Spawned Ducks").transform;
    }

    private void OnDisable() {
        Destroy(duckParent.gameObject);
    }

    private void OnDestroy() {
        if (duckParent != null)
            Destroy(duckParent.gameObject);
    }

    private void Start() {

        switch (GameManagerScript.Instance.GetCurrentMode) {
            
            case GameManagerScript.GameMode.REGULAR_MODE:
                SetRegularRound();
                break;

            default:
                SetRegularRound();
                break;
        }
        
        if (duckParent == null) {
            Debug.Log("No duck parent transform provided, creating default object");
            duckParent = new GameObject("Spawned Ducks").transform;
        }
    }

    private void SetRegularRound() {
        _ducksInRound = nbDucksPerRound;
        roundCountdown = roundDelay;
    }
    
    private void Update() {

        switch (GameManagerScript.Instance.GetCurrentMode) {
            
            case GameManagerScript.GameMode.REGULAR_MODE:
                RegularMode();
                break;
            
            default:
                RegularMode();
                break;
        }
    }

    private void RegularMode() {
        if (roundCountdown <= 0) {
            if (_ducksInRound > 0) {
                if (_ducksInWave <= 0 && !_isSpawnRoutineRunning)
                    StartCoroutine(nameof(SpawnDuckRoutine));
            }
            else{
                Debug.Log($"Round {roundNo} Over");
                roundNo++;
                SetRegularRound();
            }
        }
        else {
            roundCountdown -= Time.deltaTime;
        }
    }
    
    private IEnumerator SpawnDuckRoutine() {
        _isSpawnRoutineRunning = true;
        waveCountdown = waveDelay;
        
        while (waveCountdown > 0) {
            waveCountdown -= Time.deltaTime;
            yield return null;
        }
        
        for (int i = 0; i < nbDucksPerWave; i++) {
            
            InstantiateDuck();

            if (i > 0) {
                float nextDuck = Random.Range(0, waveDelay);
                while (nextDuck > 0) {
                    nextDuck -= Time.deltaTime;
                    yield return null;
                }
            }
        }
        
        _isSpawnRoutineRunning = false;
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
            
            if(roundNo <= 10)
                duck.GetComponent<IFlyingTarget>().FlightSpeed += flightRoundIncrement * roundNo;
            
            duck.GetComponent<IFlyingTarget>().SpanwerPos = transform.position;
            duck.GetComponent<IFlyingTarget>().SpawnSize = new Vector3(spawnSize.x / 2, spawnSize.y / 2, spawnSize.z / 2);
            duck.GetComponent<IFlyingTarget>().DiedDelegate += RemoveDuck;
            
            duck.transform.SetParent(duckParent);
            
            _ducksInWave++;
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }
    
    void RemoveDuck() {
        if(_ducksInWave > 0)
            _ducksInWave--;
        if(_ducksInRound > 0)
            _ducksInRound--;
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnSize);
    }

    public float GetRound { get => roundNo; set { roundNo = value; } }
}
