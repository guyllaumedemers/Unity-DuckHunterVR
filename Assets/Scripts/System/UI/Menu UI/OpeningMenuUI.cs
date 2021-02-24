using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class OpeningMenuUI : MonoBehaviour
{
    #region Singleton
    private static OpeningMenuUI instance;
    private OpeningMenuUI() { }
    public static OpeningMenuUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new OpeningMenuUI();
            }
            return instance;
        }
    }
    #endregion
    [Header("Requiered Components")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Toggle[] toggles;

    [Header("Scene Name")]
    private string gameSceneName = "MainGameSceneFinal";

    public void Awake()
    {
        instance = this;
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
