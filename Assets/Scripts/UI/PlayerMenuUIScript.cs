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
    private GameObject mainMenuUI;
    private Transform mainCameraTransform;
    private const string gameSceneName = "gdemersTestScene";
    private const string mainMenuSceneName = "gdemersTest-MainMenuScene";

    public void Awake()
    {
        instance = this;
        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    public void Start()
    {
        inGameMenuUI.SetActive(false);
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

    public void ActivateInGameMenuUI()
    {
        bool isLeftMenuButtonPressed = false;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out isLeftMenuButtonPressed) && isLeftMenuButtonPressed)
        {
            Debug.Log("Menu Active");
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
        // retrieve a list of data from a file
        // load the data on a canvas => better if we instanciate the canvas and fill on demand
        // swap the canvas from playerMenuUI to the Stats canvas
    }

    public void GetOnline()
    {
        // follow the online website of the game
    }

    public void DisplaySettings()
    {
        inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);
        Instantiate(mainMenuUI, new Vector3(0, 0, 10) + transform.position, Quaternion.identity, mainCameraTransform);
        if (MainMenuScript.Instance.GetMainMenuUI.activeSelf)
        {
            MainMenuScript.Instance.GetMainMenuUI.SetActive(!MainMenuScript.Instance.GetMainMenuUI.activeSelf);
        }
        MainMenuScript.Instance.GetSettingsMenuUI.SetActive(true);
    }
}
