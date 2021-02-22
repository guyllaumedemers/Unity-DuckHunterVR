using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    [Header("Requiered Components")]
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private new Animation animation;

    private readonly string start = "START";
    private readonly string stop = "STOP";

    public void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        animation = GetComponent<Animation>();
    }

    public void UpdateButton()
    {
        GameManagerScript.Instance.GetGameState = !GameManagerScript.Instance.GetGameState;
        animation.Play("PushButton");
        SwapText();
        if (GameManagerScript.Instance.GetGameState == true)
        {
            GameManagerScript.Instance.duckSpawnerClone = Instantiate(GameManagerScript.Instance.duckSpawnerPrefab, GameManagerScript.Instance.duckSpawnerPos, Quaternion.identity);
            GameManagerScript.Instance.duckSpawnerPrefab.GetComponent<DuckSpawnerController>().spawnSize = GameManagerScript.Instance.duckSpawnerSize;

            if (GameManagerScript.Instance.duckSpawnerPrefab != null)
            {
                DisplayRoundTimeUI.Instance.GetDuckSpawnerToReset = GameManagerScript.Instance.duckSpawnerPrefab.GetComponent<DuckSpawnerController>();
                DisplayRoundTimeUI.Instance.GetTimeDisplayObject.SetActive(true);
            }
            ScoringSystemManager.Instance.InstanciateNewGameInstance();
        }
        else
        {
            Destroy(GameManagerScript.Instance.duckSpawnerClone.gameObject);
            DisplayRoundTimeUI.Instance.GetTimeDisplayObject.SetActive(false);
            //ScoringSystemManager.Instance.GetGameInstance.UpdateRoundInstance(GameManagerScript.Instance.GetDuckSpawnerObject.GetComponent<DuckSpawnerController>().GetRound);
            //Serialization.SaveFile(ScoringSystemManager.Instance.GetGameInstance, Serialization.GetPath);
            ScoringSystemManager.Instance.DestroyGameInstance();            
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
