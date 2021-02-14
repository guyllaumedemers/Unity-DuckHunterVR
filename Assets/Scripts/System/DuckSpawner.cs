using UnityEngine;

public class DuckSpawner : MonoBehaviour {
    [Header("Size of spawn area")]
    public Vector3 size;
    [Header("Rate of instantiation")]
    public float spawnRate = 5f;
    [Header("Model used to instantiate")]
    public GameObject duckModel;
    [Header("Duck Parent")] 
    public Transform duckParent;
    
    private float nextSpawn = 0;
    
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
        
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), Random.Range(-90, 0));
        
        GameObject duck = Instantiate(duckModel, spawnPoint, rotation);
        duck.transform.SetParent(duckParent);
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}
