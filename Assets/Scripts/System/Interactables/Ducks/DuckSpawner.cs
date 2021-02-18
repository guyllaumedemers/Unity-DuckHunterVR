using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckSpawner : MonoBehaviour {
    
    [Header("Size of spawn area")]
    public Vector3 size;
    [Header("Model used to instantiate")]
    public GameObject[] duckModels;
    [Header("Duck Parent Transform")] 
    public Transform duckParent;
    [Header("Number of Ducks per wave")]
    public int nbDucks = 1; 
    [Header("Time betweene waves")]
    public float waveDelay = 4f;
    [Header("Wave Countdown")]
    public float waveCountdown;
    
    [SerializeField]
    private float firstWave = 10f;
    private float _ducksInWave = 0;
    private bool _isSpawnRoutine;
    
    
    private void Start() {
        if (duckParent == null) {
            Debug.Log("No duck parent transform provided, creating default object");
            duckParent = new GameObject("Spawned Ducks").transform;
        }
    }
    
    private void Update() {
        if (firstWave <= 0) {
            if(_ducksInWave <= 0 && !_isSpawnRoutine)
                StartCoroutine(nameof(SpawnDuckRoutine));
        }
        else {
            firstWave -= Time.deltaTime;
        }
    }
    
    private IEnumerator SpawnDuckRoutine() {
        _isSpawnRoutine = true;
        waveCountdown = waveDelay;
        
        while (waveCountdown > 0) {
            waveCountdown -= Time.deltaTime;
            yield return null;
        }
        
        for (int i = 0; i < nbDucks; i++) {
            
            InstantiateDuck();

            if (i > 0) {
                float nextDuck = Random.Range(0, waveDelay);
                while (nextDuck > 0) {
                    nextDuck -= Time.deltaTime;
                    yield return null;
                }
            }
        }
        
        _isSpawnRoutine = false;
    }

    private Vector3 GetRandomSpawnPoint() {
        float posX = transform.position.x + Random.Range(-size.x / 2, size.x / 2);
        float posY = transform.position.y - size.y / 2;
        float posZ = transform.position.z + Random.Range(-size.z / 2, size.z / 2);
        
        return new Vector3(posX, posY, posZ);
    }
    
    private void InstantiateDuck() {
        try {
            GameObject duck = Instantiate(duckModels[Random.Range(0, duckModels.Length)], GetRandomSpawnPoint(), Quaternion.identity);
            duck.GetComponent<IFlyingTarget>().SpanwerPos = transform.position;
            duck.GetComponent<IFlyingTarget>().SpawnSize = new Vector3(size.x / 2, size.y / 2, size.z / 2);
            duck.GetComponent<IFlyingTarget>().DiedDelegate += RemoveOneDuck;
            duck.transform.SetParent(duckParent);
            
            _ducksInWave++;
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }
    
    void RemoveOneDuck() {
        if(_ducksInWave > 0)
            _ducksInWave--;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}
