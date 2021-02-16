using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private readonly string start = "START";
    private readonly string stop = "STOP";
    private bool isRunning;

    public void Awake()
    {
        isRunning = false;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateButton()
    {
        isRunning = !isRunning;
        SwapText();
        if (isRunning)
        {
            CreateNewGameInstance.Instance.InitializeGameInstance();
            return;
        }
        CreateNewGameInstance.Instance.DestroyGameInstance();
    }

    public void SwapText()
    {
        if (textMeshProUGUI.text.Equals(stop))
        {
            textMeshProUGUI.text = start;
        }
        else
        {
            textMeshProUGUI.text = stop;
        }
    }

}
