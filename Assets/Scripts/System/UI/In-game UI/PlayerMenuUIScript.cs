using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System;

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
    [SerializeField]
    private GameObject statsMenuUI;
    private Camera mainCamera;
    private const string mainMenuSceneName = "MainMenuSceneFinal";
    private const string gameSceneName = "MainGameSceneFinal";
    private readonly double MAX_HEADSET_ROTATION_VALUE = 180;

    public void Awake()
    {
        instance = this;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Start()
    {
        inGameMenuUI.SetActive(false);
        HighScoreUI.Instance.InstanciatePlayerStatistics();
        statsMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
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
        if (settingsMenuUI != null)
        {
            return settingsMenuUI.activeSelf;
        }
        return false;
    }

    public bool IsStatsMenuUIActive()
    {
        if (statsMenuUI != null)
        {
            return statsMenuUI.activeSelf;
        }
        return false;
    }

    public void ActivateInGameMenuUI()
    {
        bool isLeftMenuButtonPressed = false;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out isLeftMenuButtonPressed) && isLeftMenuButtonPressed)
        {
            GetCameraTransformAndRotation(inGameMenuUI, mainCamera);
            inGameMenuUI.SetActive(true);
        }
    }

    public void GoBackToMainMenuScene()
    {
        inGameMenuUI.SetActive(false);
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
    }

    public void ExitGameApplication()
    {
        Application.Quit();
    }

    public void DisplayStatistics()
    {
        Vector3 recalculateStatsUIPosition = inGameMenuUI.transform.position - new Vector3(0, 2, -4);
        statsMenuUI.transform.position = recalculateStatsUIPosition;
        statsMenuUI.SetActive(true);
        inGameMenuUI.SetActive(false);
    }

    public void GetOnline()
    {
        // follow the online website of the game
    }

    public void DisplaySettings()
    {
        Vector3 temp = inGameMenuUI.transform.position;
        inGameMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        settingsMenuUI.transform.position = temp;
    }
    /// <summary>
    /// Camera Headset ONLY Track Device Rotation UP TO 180 degrees THAN fall into a range of -180 => 0;
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="camera"></param>
    public void GetCameraTransformAndRotation(GameObject gameObject, Camera camera)
    {
        double angle = camera.transform.rotation.y;
        int radius = 5; /// rotation radius around the camera yAxis
        double x = radius * Math.Cos(angle);
        double z = radius * Math.Sin(angle); /// zAxis equals vector.forward
        if (angle > 0)
        {
            gameObject.transform.position = camera.transform.position + new Vector3((float)x, 0, (float)z);
        }
        else if (angle < 0)
        {
            gameObject.transform.position = camera.transform.position - new Vector3((float)x, 0, (float)z);
        }
    }

    public void GoBackToInGameUI()
    {
        settingsMenuUI.SetActive(false);
        inGameMenuUI.SetActive(true);
    }
}
