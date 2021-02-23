using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayRoundTimeUI : MonoBehaviour
{
    [HideInInspector]public DuckSpawnerController duckSpawner;
    
    #region Singleton
    private static DisplayRoundTimeUI instance;
    private DisplayRoundTimeUI() { }
    public static DisplayRoundTimeUI Instance
    {
        get
        {
            if (instance == null)
                instance = new DisplayRoundTimeUI();
            
            return instance;
        }
    }
    #endregion

    [Header("Requiered Components")]
    [SerializeField] private GameObject roundTimer;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private bool isTimeRoundEndDisplayed;
    
    public GameObject GetTimeDisplayObject { get => roundTimer; }

    private void Awake()
    {
        instance = this;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
        roundTimer.SetActive(false);
    }

    private void OnEnable() {
        isTimeRoundEndDisplayed = false;
    }

    private void Update()
    {
        if (roundTimer.activeSelf) {
            switch (GameManagerScript.Instance.GetCurrentMode) {
                case GameManagerScript.GameMode.REGULAR_MODE:
                    RoundTimeDisplay();
                    break;
                case GameManagerScript.GameMode.TIMED_MODE:
                    if(!duckSpawner.isTimedRoundOver)
                        RoundTimeDisplay();
                    else {
                        if(!isTimeRoundEndDisplayed)
                            TimeRoundEndText();
                    }
                    break;
            }
        }
    }

    private void RoundTimeDisplay()
    {
        if (duckSpawner.roundCountdown > 0)
            UpdateText();
        else
            roundTimer.SetActive(false);
    }

    private void UpdateText() => textMeshProUGUI.text = $"ROUND {duckSpawner.roundNo} STARTS IN : {duckSpawner.roundCountdown:n0}";

    public void TimeRoundEndText() {
        textMeshProUGUI.text = $"TIMED ROUND OVER, SCORE: {ScoringSystemManager.Instance.GetGameInstance?.GetScores.GetPoints}";
        isTimeRoundEndDisplayed = true;
    }
}
