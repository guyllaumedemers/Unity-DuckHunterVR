using System;
using System.Collections;
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
    [Header("Next Duck Timer")]
    public float spawnTime;
    
    public bool DoSpawnDuck { get; set; } = false;
    public bool IsSpawnerRunning { get; private set; } = false;
    
    private float nextSpawn = 4f;
    
    private void Start() {
        if (duckParent == null) {
            Debug.Log("No duck parent transform provided, creating default object");
            duckParent = new GameObject("Spawned Ducks").transform;
        }

        StartCoroutine(nameof(SpawnDuckRoutine));
    }
    
    private void Update() {
        if (DoSpawnDuck && !IsSpawnerRunning) 
            StartCoroutine(nameof(SpawnDuckRoutine));
    }
    
    private IEnumerator SpawnDuckRoutine() {
        IsSpawnerRunning = true;
        DoSpawnDuck = false;
        
        spawnTime = nextSpawn;
        while (spawnTime > 0) {
            spawnTime -= Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < nbDucks; i++) {
            
            InstantiateDuck();

            if (i > 1) {
                spawnTime = Random.Range(0, nextSpawn);
                while (spawnTime > 0) {
                    spawnTime -= Time.deltaTime;
                    yield return null;
                }
            }
        }

        IsSpawnerRunning = false;
    }

    private Vector3 GetRandomSpawnPoint() {
        float posX = Random.Range(-size.x / 2, size.x / 2);
        float posY = 0;
        float posZ = Random.Range(-size.z / 2, size.z / 2);
        
        return new Vector3(posX, posY, posZ);
    }
    
    private void InstantiateDuck() {
        GameObject duck = Instantiate(duckModels[Random.Range(0, duckModels.Length)], GetRandomSpawnPoint(), Quaternion.identity);
        duck.GetComponent<DuckController>().duckSpawner = this;
        duck.GetComponent<DuckController>().spawnSize = size;
        duck.transform.SetParent(duckParent);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}
