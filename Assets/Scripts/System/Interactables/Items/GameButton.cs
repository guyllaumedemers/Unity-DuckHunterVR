using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private new Animation animation;
    private readonly string start = "START";
    private readonly string stop = "STOP";

    private GameObject duckSpawner;
    
    public void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        animation = GetComponent<Animation>();
        duckSpawner = GameManagerScript.Instance.duckSpawnerGo;
    }

    public void UpdateButton()
    {
        GameManagerScript.Instance.GetGameState = !GameManagerScript.Instance.GetGameState;
        animation.Play("PushButton");
        SwapText();
        if (GameManagerScript.Instance.GetGameState)
        {
            ScoringSystemManager.Instance.InstanciateNewGameInstance();
            
            Instantiate(duckSpawner, GameManagerScript.Instance.duckSpawnerPos, Quaternion.identity);
            duckSpawner.GetComponent<DuckSpawner>().spawnSize = new Vector3(10, 10, 28);
        }
        else
        {
            Destroy(duckSpawner);
            
            // Update Game Instance => Round value
            ScoringSystemManager.Instance.GetGameInstance.UpdateRoundInstance(GameManagerScript.Instance.GetDuckSpawnerObject.GetComponent<DuckSpawner>().GetRound);
            Serialization.SaveFile(ScoringSystemManager.Instance.GetGameInstance, Serialization.GetPath);
            ScoringSystemManager.Instance.DestroyGameInstance();
            // reset the RoundStatus so the player can set a new GameMode => set to false since when the instance is active the update method of the clipboard set it to true
            GameManagerScript.Instance.GetRoundStatus = !GameManagerScript.Instance.GetRoundStatus;
        }
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
