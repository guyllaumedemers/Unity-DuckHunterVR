using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScorePoints
{
    private int points;
    public ScorePoints()
    {
        points = 0;
    }

    /// <summary>
    /// Add points to the instance of the game -> pass the duck in param to retrieve the value of the duck
    /// </summary>
    public void AddPoints(int value)
    {
        points += value;
    }

    public int GetPoints { get => points; }
}
