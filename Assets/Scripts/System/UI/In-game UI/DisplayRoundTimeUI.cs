using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayRoundTimeUI : MonoBehaviour
{
    #region Singleton
    private static DisplayRoundTimeUI instance;
    private DisplayRoundTimeUI() { }
    public static DisplayRoundTimeUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DisplayRoundTimeUI();
            }
            return instance;
        }
    }
    #endregion

    [Header("Requiered Components")]
    [SerializeField] private GameObject roundTimer;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    /// <summary>
    /// Spawner controller gets set inside the GameButton Initialize function
    /// At each instanciation, the spawner controller gets a new Instance which means its internal values are than reset
    /// </summary>
    private DuckSpawnerController spawnerController;

    public void Awake()
    {
        instance = this;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
        roundTimer.SetActive(false);
    }

    public void Update()
    {
        // Check if the round is displayed
        if (roundTimer.activeSelf == true)
        {
            // Check if the duck spawner is active. if its not, we cannont retrieve the RoundCountdown Time
            if (spawnerController != null)
            {
                if (spawnerController.GetRoundCountdown > 0)
                {
                    UpdateTime(spawnerController);
                }
                else
                {
                    roundTimer.SetActive(false);
                }
            }
        }
    }

    public void UpdateTime(DuckSpawnerController duckSpawnerController)
    {
        int time = (int)duckSpawnerController.GetRoundCountdown;
        textMeshProUGUI.text = "ROUND START IN : " + time.ToString();
    }

    public GameObject GetRoundTimerObject { get => roundTimer; }

    public DuckSpawnerController GetDuckSpawner { get => spawnerController; set { spawnerController = value; } }
}
