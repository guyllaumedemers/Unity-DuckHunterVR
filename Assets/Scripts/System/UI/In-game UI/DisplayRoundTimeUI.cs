using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayRoundTimeUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
        
    private void Awake() => textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
    
    public void UpdateText(float roundNo, float roundCountdown) => textMeshProUGUI.text = $"ROUND {roundNo} STARTS IN : {roundCountdown:n0}";
    
    public void TimeRoundEndText() => textMeshProUGUI.text = $"TIMED ROUND OVER, SCORE: {ScoringSystemManager.Instance.GetGameInstance?.GetScores.GetPoints}";
}
