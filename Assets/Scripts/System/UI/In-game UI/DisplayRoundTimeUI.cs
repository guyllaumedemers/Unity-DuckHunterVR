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
    private DuckSpawnerController spawnerController;

    public void Awake()
    {
        instance = this;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
        roundTimer.SetActive(false);
    }

    public void Update()
    {
        if (roundTimer.activeSelf == true)
        {
            if (spawnerController == null)
            {
                spawnerController = GameManagerScript.Instance.duckSpawnerGo.GetComponent<DuckSpawnerController>();
            }
            StartCoroutine(StartRoundTimeDisplay(spawnerController));
        }
    }

    public void UpdateTime(DuckSpawnerController duckSpawnerController)
    {
        int time = (int)duckSpawnerController.GetRoundCountdown;
        textMeshProUGUI.text = "ROUND START IN : " + time.ToString();
    }

    public GameObject GetTimeDisplayObject { get => roundTimer; }

    public IEnumerator StartRoundTimeDisplay(DuckSpawnerController duckSpawnerController)
    {
        if (duckSpawnerController.GetRoundCountdown > 0)
        {
            UpdateTime(duckSpawnerController);
        }
        else
        {
            roundTimer.SetActive(false);
        }
        yield return null;
    }
}
