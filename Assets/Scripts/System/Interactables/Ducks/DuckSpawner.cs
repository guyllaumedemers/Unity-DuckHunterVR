using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DuckSpawner : MonoBehaviour {
    [Header("Size of spawn area")]
    public Vector3 size;
    [Header("Rate of instantiation")]
    public float spawnRate = 5f;
    [Header("Model used to instantiate")]
    public GameObject[] duckModels;
    [Header("Duck Parent Transform")] 
    public Transform duckParent;
    
    private float nextSpawn;

    private void Start() {

        nextSpawn = spawnRate;
        
        if (duckModels == null) {
            Debug.Log("No duck model provided, using default model");
            duckModels[0] = Resources.Load("Prefabs/Ducks/DuckCapsule") as GameObject;
        }

        if (duckParent == null) {
            Debug.Log("No duck parent transform provided, creating default object");
            duckParent = new GameObject("Spawned Ducks").transform;
        }
        
        SpawnDuck();
    }
    
    private void Update() {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            SpawnDuck();
        }
    }

    private void SpawnDuck() {
        Vector3 spawnPoint = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                                                              size.y,
                                                              Random.Range(-size.z / 2, size.z / 2));
        
        GameObject duck = Instantiate(duckModels[Random.Range(0, duckModels.Length)], spawnPoint, Quaternion.identity);
        
        duck.GetComponent<DuckController>().spawnSize = transform.position.x + (size.x / 2);
        duck.transform.SetParent(duckParent);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}
