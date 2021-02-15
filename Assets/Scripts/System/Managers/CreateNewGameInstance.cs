using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewGameInstance
{
    /// <summary>
    /// Player can only create one game instance, otherwise there would be problems with retriving scores from the game initialized
    /// A game instance has GameMode data and Score values
    /// When Stop button is pressed the game instance data will be saved to file and the instance will be destroyed
    /// Singleton is only there to avoid instanciating multiple instances
    /// </summary>
    #region Singleton
    private static CreateNewGameInstance instance;
    private CreateNewGameInstance() { }
    public static CreateNewGameInstance Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CreateNewGameInstance();
            }
            return instance;
        }
    }
    #endregion
    private string gameModeString;
    private ScorePoints scorePoints;
    /// <summary>
    /// Need to add a Round -> so the instance can retrieve the information
    /// </summary>
    /// <param name="gameMode"></param>
    /// <param name="points"></param>
    public CreateNewGameInstance(GameManagerScript.GameMode gameMode, ScorePoints points)
    {
        gameModeString = gameMode.ToString();
        scorePoints = points;
    }

    public void InitializeGameInstance()
    {
        instance = Instance;
    }

    public void DestroyGameInstance()
    {
        instance = null;
    }
}
