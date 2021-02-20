using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManagerScript : MonoBehaviour
{
    [System.Serializable]
    public enum GameMode
    {
        REGULAR_MODE,
        TIMED_MODE,
        CHALLENGE_MODE
    }
    #region Singleton
    private static GameManagerScript instance;
    private GameManagerScript() { }
    public static GameManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManagerScript();
            }
            return instance;
        }
    }
    #endregion

    private bool isRoundLaunch;
    private bool isRunning;
    private GameMode mode;

    [Header("Duck Spawner information")]
    public GameObject duckSpawnerGo;
    public Vector3 duckSpawnerPos = new Vector3(31f,6.5f,36f);
    public Vector3 duckSpawnerSize = new Vector3(10, 10, 28);
    
    public void Awake()
    {
        instance = this;
        mode = GameMode.REGULAR_MODE;
        isRunning = false;
        isRoundLaunch = false;
        
        //Instantiate(duckSpawnerGo, Instance.duckSpawnerPos, Quaternion.identity);
        //duckSpawnerGo.GetComponent<DuckSpawnerController>().spawnSize = duckSpawnerSize;
    }
    
    public GameMode GetCurrentMode { get => mode; set { mode = value; } }
    public bool GetRoundStatus { get => isRoundLaunch; set { isRoundLaunch = value; } }
    public bool GetGameState { get => isRunning; set { isRunning = value; } }
    public GameObject GetDuckSpawnerObject { get => duckSpawnerGo; }
}
