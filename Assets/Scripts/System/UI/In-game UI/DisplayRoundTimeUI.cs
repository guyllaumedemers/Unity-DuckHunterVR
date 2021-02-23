using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayRoundTimeUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
        
    private void Awake() => textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
    
    public void UpdateRoundText(float roundNo, float roundCountdown) => textMeshProUGUI.text = $"ROUND{DisplayRound(roundNo)} STARTS IN : {roundCountdown:n0}";

    private string DisplayRound(float round) => round > 0 ? " " + round.ToString("n0") : string.Empty;
    
    public void UpdateTimedRoundText(float timeLeft) => textMeshProUGUI.text = $"{timeLeft:0}";
    
    public void TimeRoundEndText() => textMeshProUGUI.text = $"TIMED ROUND SCORE: {ScoringSystemManager.Instance.GetGameInstance?.GetScores.GetPoints}";
}
