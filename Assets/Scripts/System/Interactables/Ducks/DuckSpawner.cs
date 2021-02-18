using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
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
    
    private readonly List<GameObject> duckList = new List<GameObject>();
    private bool isSpawnDuckRunning;
    
    private void Start() {
        if (duckParent == null) {
            Debug.Log("No duck parent transform provided, creating default object");
            duckParent = new GameObject("Spawned Ducks").transform;
        }

        StartCoroutine(nameof(SpawnDuckRoutine));
    }
    
    private void Update() {
        duckList.RemoveAll(item => item == null);
        
        if (duckList.Count == 0 && !isSpawnDuckRunning)
            StartCoroutine(nameof(SpawnDuckRoutine));
    }
    
    private IEnumerator SpawnDuckRoutine() {
        isSpawnDuckRunning = true;
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
        
        isSpawnDuckRunning = false;
    }

    private Vector3 GetRandomSpawnPoint() {
        float posX = Random.Range(-size.x / 2, size.x / 2);
        float posY = 0;
        float posZ = Random.Range(-size.z / 2, size.z / 2);
        
        return new Vector3(posX, posY, posZ);
    }
    
    private void InstantiateDuck() {
        try {
            GameObject duck = Instantiate(duckModels[Random.Range(0, duckModels.Length)], GetRandomSpawnPoint(), Quaternion.identity);
            duck.GetComponent<DuckController>().spawnSize = size;
            duck.transform.SetParent(duckParent);
            duckList.Add(duck);
        }
        catch (Exception ex){
            Debug.Log(ex);
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}
