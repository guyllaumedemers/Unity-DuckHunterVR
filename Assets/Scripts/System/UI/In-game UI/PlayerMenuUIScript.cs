using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class PlayerMenuUIScript : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Instance
    /// </summary>
    private static PlayerMenuUIScript instance;
    /// <summary>
    /// Default Constructor
    /// </summary>
    private PlayerMenuUIScript() { }
    /// <summary>
    /// Property to retrieve instance
    /// </summary>
    public static PlayerMenuUIScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerMenuUIScript();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField]
    private GameObject inGameMenuUI;
    [SerializeField]
    private GameObject settingsMenuUI;
    private Camera mainCamera;
    private const string mainMenuSceneName = "MainMenuSceneFinal";
    private const string gameSceneName = "MainGameSceneFinal";
    private GameObject settingsMenuUIInstance;

    public void Awake()
    {
        instance = this;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Start()
    {
        inGameMenuUI.SetActive(false);
        HighScoreUI.Instance.InstanciatePlayerStatistics();
    }

    public bool IsInGameScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.IsValid() && currentScene.name.Equals(gameSceneName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsSettingsMenuUIActive()
    {
        if (settingsMenuUIInstance != null)
        {
            return settingsMenuUIInstance.activeSelf;
        }
        return false;
    }

    public void ActivateInGameMenuUI()
    {
        bool isLeftMenuButtonPressed = false;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out isLeftMenuButtonPressed) && isLeftMenuButtonPressed)
        {
            inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);
        }
    }

    public void GoBackToMainMenuScene()
    {
        inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
    }

    public void ExitGameApplication()
    {
        Application.Quit();
    }

    public void DisplayStatistics()
    {

    }

    public void GetOnline()
    {
        // follow the online website of the game
    }

    public void DisplaySettings()
    {
        //inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);
        //if (settingsMenuUIInstance == null)
        //{
        //    settingsMenuUIInstance = Instantiate(settingsMenuUI, mainCamera.transform.position + new Vector3(0, 0, 20), Quaternion.identity, mainCamera.transform);
        //    settingsMenuUIInstance.GetComponent<Canvas>().worldCamera = mainCamera;
        //}
    }
}
