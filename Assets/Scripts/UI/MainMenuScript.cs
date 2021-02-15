using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MainMenuScript : MonoBehaviour
{
    #region Singleton
    private static MainMenuScript instance;
    private MainMenuScript() { }
    public static MainMenuScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MainMenuScript();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Toggle disableSounds;
    private string gameSceneName = "MainGameScene";

    public void Awake()
    {
        instance = this;
        disableSounds.isOn = false;
    }

    public void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    #region OnClick Event interaction
    public void LaunchGame()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }

    public void AccessSettings()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void GoBack()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    public GameObject GetMainMenuUI { get => mainMenu; }
    public GameObject GetSettingsMenuUI { get => settingsMenu; }
}
