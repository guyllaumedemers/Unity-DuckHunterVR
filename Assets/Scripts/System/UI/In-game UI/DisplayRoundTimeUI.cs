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
    
    public GameObject GetTimeDisplayObject { get => roundTimer; }
        
    public void Awake()
    {
        instance = this;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
        roundTimer.SetActive(false);
    }

    public void Update()
    {
        if (roundTimer.activeSelf)
        {
            if (duckSpawner != null)
                RoundTimeDisplay();
        }
    }
    
    public void RoundTimeDisplay()
    {
        if (duckSpawner.roundCountdown > 0)
            UpdateText();
        else
            roundTimer.SetActive(false);
    }
    
    public void UpdateText() => textMeshProUGUI.text = $"ROUND {duckSpawner.roundNo} STARTS IN : {duckSpawner.roundCountdown:n0}";
}
