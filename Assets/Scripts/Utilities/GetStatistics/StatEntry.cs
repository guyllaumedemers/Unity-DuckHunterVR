using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{
    [Header("Required Components")]
    private TextMeshProUGUI[] textMeshProUGUI;

    public void Awake()
    {
        textMeshProUGUI = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void InitializeDataEntry(CreateNewGameInstance instance, int rank)
    {
        textMeshProUGUI[0].text = DisplayRank(rank);
        textMeshProUGUI[1].text = instance.GetScores.GetPoints.ToString();
        textMeshProUGUI[2].text = instance.GetRound.ToString();
        textMeshProUGUI[3].text = instance.GetGameMode.ToString();
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
