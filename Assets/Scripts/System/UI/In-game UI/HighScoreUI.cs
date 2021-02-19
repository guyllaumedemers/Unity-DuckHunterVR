using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    #region Singleton
    private static HighScoreUI instance;
    private HighScoreUI() { }
    public static HighScoreUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HighScoreUI();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField]
    private Canvas canvas;
    [SerializeField] private GameObject statEntry;
    private CreateNewGameInstance[] highScoreArr = new CreateNewGameInstance[10];
    private const int MAX_NUMBER_OF_SCORE_DISPLAY = 10;

    public void Awake()
    {
        instance = this;
        canvas = GetComponent<Canvas>();
    }

    public void InstanciatePlayerStatistics()
    {
        List<CreateNewGameInstance> instances = Serialization.Load(Serialization.GetPath);
        CreateNewGameInstance[] myArr = BubbleSortArray(instances.ToArray());
        // I need to go thru all the instances -> compare them to find the highest score
        // remove the highest score from the list and loop the process until my top10 is filled
        // update the go object with the top 10 values
        Transform target = GameObject.FindGameObjectWithTag("UIScore").GetComponent<Transform>();
        int i = 0;
        while (i < myArr.Length && i < MAX_NUMBER_OF_SCORE_DISPLAY)
        {
            GameObject go = Instantiate(statEntry, target);
            TextMeshProUGUI[] textMeshProUGUI = go.GetComponentsInChildren<TextMeshProUGUI>();
            int value = i + 1;
            textMeshProUGUI[0].text = DisplayRank(value);
            textMeshProUGUI[1].text = myArr[i].GetScores.GetPoints.ToString();
            textMeshProUGUI[2].text = myArr[i].GetGameMode.ToString();
            i++;
        }
    }
    /// <summary>
    /// Everything is pointer => also change sign for swaping so the highest score is on top
    /// </summary>
    /// <param name="instances"></param>
    public CreateNewGameInstance[] BubbleSortArray(CreateNewGameInstance[] myArr)
    {
        for (int i = 0; i < myArr.Length - 1; i++)
        {
            for (int j = myArr.Length - 1; j > i; j--)
            {
                if (myArr[i].GetScores.GetPoints < myArr[j].GetScores.GetPoints)
                {
                    SwapIndexValue(myArr, i, j);
                }
            }
        }
        return myArr;
    }

    public void SwapIndexValue(CreateNewGameInstance[] myArr, int indexToSwap, int index)
    {
        CreateNewGameInstance temp = myArr[index];
        myArr[index] = myArr[indexToSwap];
        myArr[indexToSwap] = temp;
    }

    public string DisplayRank(int value)
    {
        string rankString;
        rankString = value switch
        {
            1 => value.ToString() + "ST",
            2 => value.ToString() + "ND",
            3 => value.ToString() + "RD",
            _ => value.ToString() + "TH",
        };
        return rankString;
    }
}
