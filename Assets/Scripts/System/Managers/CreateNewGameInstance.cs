using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreateNewGameInstance
{
    [JsonProperty]
    private GameManagerScript.GameMode gameMode;
    [JsonProperty]
    private ScorePoints scorePoints;
    /// <summary>
    /// Need to add a Round -> so the instance can retrieve the information 
    /// </summary>
    /// <param name="gameMode"></param>
    /// <param name="points"></param>
    public CreateNewGameInstance()
    {
        gameMode = GameManagerScript.Instance.GetCurrentMode;
        scorePoints = new ScorePoints();
    }
    [JsonIgnore]
    public ScorePoints GetScores { get => scorePoints; set { scorePoints = value; } }
    [JsonIgnore]
    public GameManagerScript.GameMode GetGameMode { get => gameMode; set { gameMode = value; } }
}
