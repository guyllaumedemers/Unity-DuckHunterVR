using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Database : MonoBehaviour
{
    #region Singleton
    private static Database instance;
    private Database() { }
    public static Database Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Database();
            }
            return instance;
        }
    }
    #endregion
    [Header("Required Components")]
    [SerializeField] private GameObject statEntry;

    private List<GameObject> entries;
    private const int MAX_NUMBER_OF_SCORE_DISPLAY = 10;
    private const int INITIAL_RANK_VALUE = 1;

    public void Awake()
    {
        instance = this;
        entries = new List<GameObject>();
    }

    public void InstanciatePlayerStatistics()
    {
        DeleteEntriesGameObject(entries);
        List<CreateNewGameInstance> instances = Serialization.Load(Serialization.GetPath);
        CreateNewGameInstance[] myArr = BubbleSortArray(instances.ToArray());

        Transform target = GameObject.FindGameObjectWithTag("UIScore").GetComponent<Transform>();
        int i = 0;
        while (i < myArr.Length && i < MAX_NUMBER_OF_SCORE_DISPLAY)
        {
            StatEntry entry = Instantiate(statEntry, target).GetComponent<StatEntry>();
            entries.Add(entry.gameObject);
            entry.InitializeDataEntry(myArr[i], i + INITIAL_RANK_VALUE);
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

    public void DeleteEntriesGameObject(List<GameObject> entries)
    {
        foreach (GameObject go in entries)
        {
            Destroy(go);
        }
        entries.Clear();
    }
}
