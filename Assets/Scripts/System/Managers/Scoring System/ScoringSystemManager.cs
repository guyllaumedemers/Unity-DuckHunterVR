using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystemManager : MonoBehaviour
{
    #region Singleton
    private static ScoringSystemManager instance;
    private ScoringSystemManager() { }
    public static ScoringSystemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoringSystemManager();
            }
            return instance;
        }
    }
    #endregion
    /// <summary>
    /// Check to see if the ref is needed since the instance is pass as copy, by calling AddPoints are we updating the copy of the instance or the isntance itself
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    public void AddPoints(ref CreateNewGameInstance instance, int value)
    {
        instance.GetScores.AddPoints(value);
    }
    /// <summary>
    /// Retrieve the information attach to the game currently running -> What Game Mode are we in? -> How many Round have we played -> What is the current Score for this instance of the game?
    /// We will need to access a canvas to update the billboard and display the current values of this instance
    /// </summary>
    public void GetScore(CreateNewGameInstance instance)
    {
    }
    /// <summary>
    /// Load All instances saved in file to display in Stats Settings
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public List<CreateNewGameInstance> Load(string path)
    {
        return Serialization.Load(path);
    }
}
