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
    [Header("Required Components")]
    [SerializeField] private GameObject statEntry;

    private List<CreateNewGameInstance> instances;
    private const int MAX_NUMBER_OF_SCORE_DISPLAY = 10;

    public void Awake()
    {
        instance = this;
    }

    public void InstanciatePlayerStatistics()
    {
        instances.Clear();
        instances = Serialization.Load(Serialization.GetPath);
        CreateNewGameInstance[] myArr = BubbleSortArray(instances.ToArray());

        Transform target = GameObject.FindGameObjectWithTag("UIScore").GetComponent<Transform>();
        int i = 0;
        while (i < myArr.Length && i < MAX_NUMBER_OF_SCORE_DISPLAY)
        {
            GameObject go = Instantiate(statEntry, target);
            TextMeshProUGUI[] textMeshProUGUI = go.GetComponentsInChildren<TextMeshProUGUI>(true);
            int value = i + 1;
            textMeshProUGUI[0].text = DisplayRank(value);
            textMeshProUGUI[1].text = myArr[i].GetScores.GetPoints.ToString();
            textMeshProUGUI[2].text = myArr[i].GetRound.ToString();
            textMeshProUGUI[3].text = myArr[i].GetGameMode.ToString();
            i++;
        }
    }

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
