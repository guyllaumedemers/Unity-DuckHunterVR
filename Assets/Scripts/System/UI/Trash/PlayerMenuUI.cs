using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PlayerMenuUI : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Instance
    /// </summary>
    private static PlayerMenuUI instance;
    /// <summary>
    /// Default Constructor
    /// </summary>
    private PlayerMenuUI() { }
    /// <summary>
    /// Property to retrieve instance
    /// </summary>
    public static PlayerMenuUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerMenuUI();
            }
            return instance;
        }
    }
    #endregion
    [Header("Requiered Components")]
    [SerializeField] private GameObject inGameMenuUI;
    [SerializeField] private GameObject statsMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private Camera mainCamera;
    private Toggle[] toggles;

    [Header("Scene Name")]
    private const string mainMenuSceneName = "MainMenuSceneFinal";
    private const string gameSceneName = "MainGameSceneFinal";

    public void Awake()
    {
        instance = this;
        inGameMenuUI = GameObject.FindGameObjectWithTag("IngameMenuUI");
        statsMenuUI = GameObject.FindGameObjectWithTag("StatsMenuUI");
        settingsMenuUI = GameObject.FindGameObjectWithTag("SettingsMenuUI");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Start()
    {
        toggles = settingsMenuUI.GetComponentsInChildren<Toggle>();
        SetInactive(new GameObject[] { inGameMenuUI, settingsMenuUI, statsMenuUI });
    }

    #region Scene Checks
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
    #endregion

    public void ActivateInGameMenuUI()
    {
        bool isLeftMenuButtonPressed = false;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out isLeftMenuButtonPressed) && isLeftMenuButtonPressed)
        {
            Utilities.GetCameraTransformAndRotation(inGameMenuUI, mainCamera);
            inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);
        }
    }

    #region Button Actions
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
        Utilities.GetCameraTransformAndRotation(statsMenuUI, mainCamera);
        InvertActiveUIValues(inGameMenuUI, statsMenuUI);
        HighScoreUI.Instance.InstanciatePlayerStatistics();
    }

    public void GetOnline()
    {
        // follow the online website of the game
    }

    public void DisplaySettings()
    {
        Utilities.GetCameraTransformAndRotation(settingsMenuUI, mainCamera);
        InvertActiveUIValues(inGameMenuUI, settingsMenuUI);
    }

    public void GoBackToInGameUI()
    {
        SetInactive(new GameObject[] { settingsMenuUI, statsMenuUI });
        inGameMenuUI.SetActive(true);
    }
    #endregion

    public void SetInactive(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
    }

    public void InvertActiveUIValues(GameObject inactive, GameObject active)
    {
        inactive.SetActive(!inactive.activeSelf);
        active.SetActive(!active.activeSelf);
    }
}
