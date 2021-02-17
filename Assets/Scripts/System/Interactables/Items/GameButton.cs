using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private new Animation animation;
    private readonly string start = "START";
    private readonly string stop = "STOP";
    private bool isRunning;

    public void Awake()
    {
        isRunning = false;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        animation = GetComponent<Animation>();
    }

    public void UpdateButton()
    {
        isRunning = !isRunning;
        animation.Play("PushButton");
        SwapText();
        if (isRunning)
        {
            ScoringSystemManager.Instance.InstanciateNewGameInstance();
            return;
        }
        Serialization.SaveFile(ScoringSystemManager.Instance.GetGameInstance, Serialization.GetPath);
        ScoringSystemManager.Instance.DestroyGameInstance();
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
