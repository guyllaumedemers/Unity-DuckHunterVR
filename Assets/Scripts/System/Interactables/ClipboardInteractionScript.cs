using System;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardInteractionScript : MonoBehaviour
{
    [SerializeField]
    private Toggle[] toggles;

    private Toggle _currentToggle;
    
    public void Awake()
    {
        
        // set delegates to look for Toggle value Changes
        foreach (Toggle t in toggles)
            t.onValueChanged.AddListener((a) => { ToggleValueChanged(t); });
        
        ActivateRegularModeOnAwake();
    }
    
    // public void Update()
    // {
    //     if (!GameManagerScript.Instance.GetRoundStatus)
    //     {
    //         SetGameMode((GameManagerScript.GameMode)GetIndexWithToggleIsOnActive());
    //         if (ScoringSystemManager.Instance.GetGameInstance != null)
    //         {
    //             // we could set the Round Status to true only when a button is press to tell the game the player has pick and confirm his game mode
    //             // but for now its fine with setting only once
    //             GameManagerScript.Instance.GetRoundStatus = true;
    //         }
    //     }
    // }

    public void ActivateRegularModeOnAwake()
    {
        Toggle regularModeToggle = GameObject.FindGameObjectWithTag("RegularMode").GetComponent<Toggle>();
        foreach (Toggle t in toggles) {
            t.isOn = false;
            if (t.Equals(regularModeToggle))
                t.isOn = true;
        }
    }
    
    /// <summary>
    /// Retrieve the index of the Game Mode Select
    /// </summary>
    /// <returns></returns>
    // public int GetIndexWithToggleIsOnActive()
    // {
    //     int index = -1;
    //     for (int i = 0; i < toggles.Length; i++)
    //     {
    //         if (toggles[i].isOn)
    //         {
    //             index = i;
    //             break;
    //         }
    //     }
    //     return index;
    // }

    // public void SetGameMode(GameManagerScript.GameMode mode)
    // {
    //     try
    //     {
    //         GameManagerScript.Instance.GetCurrentMode = mode;
    //     }
    //     catch (IndexOutOfRangeException e)
    //     {
    //         Debug.Log("" + e.Message);
    //     }
    // }

    void ToggleValueChanged(Toggle toggle) {
        GameManagerScript.Instance.CurrentMode = toggle.tag.ToEnum<GameMode.Mode>();
        
        for (int i = 0; i < toggles.Length; i++) {
            if (!toggles[i].Equals(toggle))
                toggles[i].SetIsOnWithoutNotify(false);
        }
    }
}
