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
    [Header("Requiered Components")]
    [SerializeField] private GameObject inGameMenuUI;
    [SerializeField] private GameObject statsMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    private Camera mainCamera;

    [Header("Scene Name")]
    private const string mainMenuSceneName = "MainMenuSceneFinal";
    private const string gameSceneName = "MainGameSceneFinal";

    public void Awake()
    {
        instance = this;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Start()
    {
        HighScoreUI.Instance.InstanciatePlayerStatistics(); // needs to be first otherwise the data wont load
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
            GetCameraTransformAndRotation(inGameMenuUI, mainCamera);
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
        GetCameraTransformAndRotation(statsMenuUI, mainCamera);
        InvertActiveUIValues(inGameMenuUI, statsMenuUI);
    }

    public void GetOnline()
    {
        // follow the online website of the game
    }

    public void DisplaySettings()
    {
        GetCameraTransformAndRotation(settingsMenuUI, mainCamera);
        InvertActiveUIValues(inGameMenuUI, settingsMenuUI);
    }

    public void GoBackToInGameUI()
    {
        SetInactive(new GameObject[] { settingsMenuUI, statsMenuUI });
        inGameMenuUI.SetActive(true);
    }
    #endregion

    public void GetCameraTransformAndRotation(GameObject gameObject, Camera camera)
    {
        Vector3 angle = camera.transform.rotation.eulerAngles;
        double rad = (Math.PI / 180) * angle.y;
        int radius = 5; /// rotation radius around the camera yAxis
        double z = radius * Math.Cos(rad);
        double x = radius * Math.Sin(rad); /// zAxis equals vector.forward
        gameObject.transform.position = camera.transform.position + new Vector3((float)x, 0, (float)z);
        gameObject.transform.rotation = Quaternion.Euler(0, angle.y, 0);
    }

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
