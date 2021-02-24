using TMPro;
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
        
        if (GameManagerScript.Instance.GetGameState)
            StartGame();
        else
            StopGame();
    }

    public void StartGame()
    {
        GameManagerScript.Instance.StartDuckSpawner();
        ScoringSystemManager.Instance.InstanciateNewGameInstance();
    }

    public void StopGame()
    {
        GameManagerScript.Instance.StopDuckSpawner();
        ScoringSystemManager.Instance.GetGameInstance?.UpdateInstanceRoundValue(GameManagerScript.Instance.duckSpawnerController.roundNo);
        
        Serialization.SaveFile(ScoringSystemManager.Instance.GetGameInstance, Serialization.GetPath);

        ScoringSystemManager.Instance.DestroyGameInstance();
        GameManagerScript.Instance.GetRoundStatus = !GameManagerScript.Instance.GetRoundStatus;
    }
    
    public void SwapText()
    {
        if (textMeshProUGUI.text.Equals(stop))
            textMeshProUGUI.text = start;
        else
            textMeshProUGUI.text = stop;
    }
}
