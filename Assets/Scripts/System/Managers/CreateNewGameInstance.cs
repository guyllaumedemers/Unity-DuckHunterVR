using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewGameInstance
{
    private string gameModeString;
    private ScorePoints scorePoints;
    /// <summary>
    /// Need to add a Round -> so the instance can retrieve the information 
    /// </summary>
    /// <param name="gameMode"></param>
    /// <param name="points"></param>
    public CreateNewGameInstance()
    {
        gameModeString = GameManagerScript.Instance.GetCurrentMode.ToString();
        scorePoints = new ScorePoints();
    }

    public ScorePoints GetScores { get => scorePoints; set { scorePoints = value; } }
}
