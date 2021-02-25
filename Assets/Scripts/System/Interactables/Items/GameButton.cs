using TMPro;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    [Header("Requiered Components")]
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

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

    public void StartGame()
    {
        GameManager.Instance.StartDuckSpawner();
        ScoringSystemManager.Instance.InstanciateNewGameInstance();
    }

    public void StopGame()
    {
        GameManager.Instance.StopDuckSpawner();
        ScoringSystemManager.Instance.GetGameInstance?.UpdateInstanceRoundValue(GameManager.Instance.duckSpawnerController.roundNo);
        
        Serialization.SaveFile(ScoringSystemManager.Instance.GetGameInstance, Serialization.GetPath);

        ScoringSystemManager.Instance.DestroyGameInstance();
        GameManager.Instance.GetRoundStatus = !GameManager.Instance.GetRoundStatus;
    }
    
    public void SwapText()
    {
        if (textMeshProUGUI.text.Equals(stop))
            textMeshProUGUI.text = start;
        else
            textMeshProUGUI.text = stop;
    }
}
