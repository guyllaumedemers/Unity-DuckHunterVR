using TMPro;
using UnityEngine;

public class ScoringSystemManager : Singleton<ScoringSystemManager>
{
    [Header("Requiered Components")]
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private CreateNewGameInstance playerGameInstance;

    protected override void Awake()
    {
        base.Awake();
        textMeshProUGUI = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (GameManager.Instance.GetGameState)
        {
            //textMeshProUGUI.text = playerGameInstance.GetScores?.GetPoints.ToString();
        }
    }

    public void InstanciateNewGameInstance()
    {
        playerGameInstance = new CreateNewGameInstance();
    }

    public void DestroyGameInstance()
    {
        playerGameInstance = new CreateNewGameInstance();
        playerGameInstance = null;
    }

    /// <summary>
    /// Check to see if the ref is needed since the instance is pass as copy, by calling AddPoints are we updating the copy of the instance or the isntance itself
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="value"></param>
    public void AddPoints(CreateNewGameInstance instance, int value)
    {
        instance.GetScores.AddPoints(value);
    }
    /// <summary>
    /// Retrieve the information attach to the game currently running -> What Game Mode are we in? -> How many Round have we played -> What is the current Score for this instance of the game?
    /// We will need to access a canvas to update the billboard and display the current values of this instance
    /// </summary>
    public int GetScore(CreateNewGameInstance instance)
    {
        return instance.GetScores.GetPoints;
    }

    public CreateNewGameInstance GetGameInstance { get => playerGameInstance; }
}
