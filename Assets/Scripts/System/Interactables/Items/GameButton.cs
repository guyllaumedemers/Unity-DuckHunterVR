using TMPro;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    [Header("Requiered Components")]
    public TextMeshProUGUI textMeshProUGUI;

    private readonly string start = "START";
    private readonly string stop = "STOP";

    public void UpdateButton()
    {
        GameManager.Instance.GetGameState = !GameManager.Instance.GetGameState;
        GetComponent<Animation>().Play("PushButton");
        SwapText();

        if (GameManager.Instance.GetGameState)
            StartGame();
        else
            StopGame();
    }

    /// <summary>
    /// DO NOT CHANGE THE ORDER
    /// </summary>
    public void StartGame()
    {
        GameManager.Instance.StartDuckSpawner();
        ScoringSystemManager.Instance.InstanciateNewGameInstance();
    }

    /// <summary>
    /// DO NOT CHANGE THE ORDER
    /// </summary>
    public void StopGame()
    {
        if (GameManager.Instance.duckSpawnerController != null)
        {
            ScoringSystemManager.Instance.GetGameInstance?.UpdateInstanceRoundValue(GameManager.Instance.duckSpawnerController.roundNo);
            GameManager.Instance.StopDuckSpawner();

            Serialization.SaveFile(ScoringSystemManager.Instance.GetGameInstance, Serialization.GetPath);
            ScoringSystemManager.Instance.DestroyGameInstance();
        }
    }

    public void SwapText()
    {
        if (textMeshProUGUI.text.Equals(stop))
            textMeshProUGUI.text = start;
        else
            textMeshProUGUI.text = stop;
    }
}
