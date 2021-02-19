using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringSystemManager : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private CreateNewGameInstance newGame;
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
    public void Awake()
    {
        instance = this;
        textMeshProUGUI = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (GameManagerScript.Instance.GetGameState)
        {
            textMeshProUGUI.text = newGame.GetScores.GetPoints.ToString();
        }
    }

    public void InstanciateNewGameInstance()
    {
        newGame = new CreateNewGameInstance();
    }

    public void DestroyGameInstance()
    {
        newGame = new CreateNewGameInstance();
        newGame = null;
    }

    /// <summary>
    /// Check to see if the ref is needed since the instance is pass as copy, by calling AddPoints are we updating the copy of the instance or the isntance itself
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    public void AddPoints(CreateNewGameInstance instance, int value)
    {
        instance.GetScores.AddPoints(value);
    }
    /// <summary>
    /// Retrieve the information attach to the game currently running -> What Game Mode are we in? -> How many Round have we played -> What is the current Score for this instance of the game?
    /// We will need to access a canvas to update the billboard and display the current values of this instance
    /// </summary>
    public int GetScore(CreateNewGameInstance instance)
    {
        return instance.GetScores.GetPoints;
    }

    public CreateNewGameInstance GetGameInstance { get => newGame; }
}
