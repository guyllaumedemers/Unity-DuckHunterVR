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
        GameManagerScript.Instance.GetGameState = !GameManagerScript.Instance.GetGameState;
        animation.Play("PushButton");
        SwapText();
        if (GameManagerScript.Instance.GetGameState)
        {
            ScoringSystemManager.Instance.InstanciateNewGameInstance();
            return;
        }
        Serialization.SaveFile(ScoringSystemManager.Instance.GetGameInstance, Serialization.GetPath);
        ScoringSystemManager.Instance.DestroyGameInstance();
        // reset the RoundStatus so the player can set a new GameMode => set to false since when the instance is active the update method of the clipboard set it to true
        GameManagerScript.Instance.GetRoundStatus = !GameManagerScript.Instance.GetRoundStatus;
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
