using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreateNewGameInstance
{
    [JsonProperty]
    private GameMode.Mode gameMode;
    [JsonProperty]
    private ScorePoints scorePoints;
    [JsonProperty]
    private float roundNo;
    private const float startRoundNo = 1;
    /// <summary>
    /// Need to add a Round -> so the instance can retrieve the information 
    /// </summary>
    /// <param name="gameMode"></param>
    /// <param name="points"></param>
    public CreateNewGameInstance()
    {
        gameMode = GameManagerScript.Instance.CurrentMode;
        scorePoints = new ScorePoints();
        roundNo = startRoundNo;
    }

    public void UpdateInstanceRoundValue(float nbRound)
    {
        roundNo = nbRound;
    }

    [JsonIgnore]
    public ScorePoints GetScores { get => scorePoints; set { scorePoints = value; } }
    [JsonIgnore]
    public GameMode.Mode GetGameMode { get => gameMode; set { gameMode = value; } }
    [JsonIgnore]
    public float GetRound { get => roundNo; }
}
