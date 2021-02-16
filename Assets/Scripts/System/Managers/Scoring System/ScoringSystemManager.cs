using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringSystemManager : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private bool isEarningPoints = false;
    private bool isActive = false;
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
    /// Would be best to only do a check when the a duck is killed
    /// </summary>
    public void Update()
    {
        if (isActive)
        {
            textMeshProUGUI.text = GetScore(CreateNewGameInstance.Instance).ToString();
        }
    }

    public void InitializeScoringSystem()
    {
        instance = this;
        isEarningPoints = true;
        isActive = true;
        textMeshProUGUI = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        StartCoroutine(AddPointCoroutine());
    }

    public void DestroyScoringSystem()
    {
        isActive = false;
        textMeshProUGUI.text = "" + 0;
        isEarningPoints = false;
        instance = null;
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

    /// <summary>
    /// Testing -> Just realized that by calling the instance of CreateNewGame I instanciate it which means the GameButton isnt in control of the instance
    /// which is why the score board constantly update
    /// I need to initialize the scoring system only when the GameButton is Selected
    /// </summary>
    /// <returns></returns>
    public IEnumerator AddPointCoroutine()
    {
        while (isEarningPoints)
        {
            yield return new WaitForSeconds(3.0f);
            AddPoints(CreateNewGameInstance.Instance, 10);
            Debug.Log("Instance points : " + CreateNewGameInstance.Instance.GetScores.GetPoints);
        }
    }
}
