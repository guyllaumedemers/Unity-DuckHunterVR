using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class MenuUI : MonoBehaviour
{
    #region Singleton
    private static MenuUI instance;
    private MenuUI() { }
    public static MenuUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MenuUI();
            }
            return instance;
        }
    }
    #endregion

    [Header("Requiered Components")]
    [SerializeField] private GameObject[] menus;
    [SerializeField] private GameObject current;
    [Tooltip("Target is always going to be refering to Settings Menu UI for convenience sake")]
    [SerializeField] private GameObject target;
    [SerializeField] private new Camera camera;

    [Header("Canvas Tag")]
    private readonly string CURRENT_MAIN_MENU_UI = "MainMenuUI";
    private readonly string TARGET_SETTINGS_MENU_UI = "SettingsMenuUI";
    private readonly string CURRENT_GAME_MENU_UI = "IngameMenuUI";
    private readonly string TARGET_STATS_MENU_UI = "StatsMenuUI";

    [Header("Scene Name")]
    private readonly string TARGET_SCENE = "MainGameSceneFinal";
    private readonly string PREVIOUS_SCENE = "MainMenuSceneFinal";

    [Header("Settings Menu Components")]
    [SerializeField] private Slider[] sliders;
    [SerializeField] private Toggle[] toggles;

    [Header("Previous Settings to retrieve when changing scene")]
    private bool isInitialize;

    [Header("Camera Tag")]
    private readonly string CAMERA_TAG = "MainCamera";

    [Header("Slider Tag")]
    private readonly string MUSIC_SLIDER = "MusicSlider";
    private readonly string SFX_SLIDER = "SfxSlider";

    [Header("Toggle Tag")]
    private readonly string DISABLE_SOUND_TOGGLE = "DisableSound";
    private readonly string ENABLE_GORE_TOGGLE = "EnableGore";

    private void Awake()
    {
        instance = this;
        isInitialize = true;
        camera = GameObject.FindGameObjectWithTag(CAMERA_TAG).GetComponent<Camera>();
        DisableAllUI();
    }

    private void Start()
    {
        InitializeAllComponents();
    }

    private void Update()
    {
        if (target != null)
        {
            UpdateUIElements();
        }
    }

    #region Actions
    private void DisableAllUI()
    {
        foreach (GameObject go in menus)
        {
            go.SetActive(false);
        }
    }
    private void InitializeAllComponents()
    {
        if (!SceneManager.GetActiveScene().name.Equals(TARGET_SCENE))
        {
            current = GetUIWithTag(CURRENT_MAIN_MENU_UI);
            current.SetActive(true);
        }
        else
        {
            current = GetUIWithTag(CURRENT_GAME_MENU_UI);
        }
        target = GetUIWithTag(TARGET_SETTINGS_MENU_UI);
        if (target != null)
        {
            sliders = target.GetComponentsInChildren<Slider>();
            toggles = target.GetComponentsInChildren<Toggle>();
        }
    }
    private void SetUIElementsToSavedValues()
    {
        Slider music = GetSliderWithTag(MUSIC_SLIDER);
        music.value = AudioManager.Instance.GetMusicVolume;

        Slider sfx = GetSliderWithTag(SFX_SLIDER);
        sfx.value = AudioManager.Instance.GetSFXVolume;
    }
    private void UpdateUIElements()
    {
        if (target.CompareTag(TARGET_SETTINGS_MENU_UI) && target.activeSelf)
        {
            if (isInitialize)
            {
                SetUIElementsToSavedValues();
                isInitialize = false;
            }
        }
    }
    private GameObject GetUIWithTag(string tag)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].CompareTag(tag))
            {
                return menus[i];
            }
        }
        return null;
    }
    private Slider GetSliderWithTag(string tag)
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].CompareTag(tag))
            {
                return sliders[i];
            }
        }
        return null;
    }
    private Toggle GetToggleWithTag(string tag)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].CompareTag(tag))
            {
                return toggles[i];
            }
        }
        return null;
    }
    #endregion

    public void ActivateInGameMenuUI()
    {
        bool isLeftMenuButtonPressed = false;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out isLeftMenuButtonPressed) && isLeftMenuButtonPressed)
        {
            Utilities.GetCameraTransformAndRotation(current, camera);
            current.SetActive(!current.activeSelf);
        }
    }

    public void GetGameSceneUICallback()
    {
        if (SceneManager.GetActiveScene().name.Equals(TARGET_SCENE))
        {
            ActivateInGameMenuUI();
        }
    }

    /************************************************************************************/
    #region OnClick Event => Opening Menu Interaction
    public void LaunchGame()
    {
        SceneManager.LoadScene(TARGET_SCENE, LoadSceneMode.Single);
    }
    public void AccessSettings()
    {
        Utilities.GetCameraTransformAndRotation(target, camera);
        current.SetActive(false);
        target.SetActive(true);
    }
    /// <summary>
    /// If we are in the settings Menu we can simply go back by settings our active self to the invert since we are currently active
    /// but if the target is NOT the Settings Menu, meaning that the actif true is some other UI, then we have to retrieve the active
    /// gameobject and change its state
    /// </summary>
    public void GoBack()
    {
        GameObject temp_target = target;
        foreach (GameObject go in menus)
        {
            if (go.activeSelf)
            {
                temp_target = go;
                break;
            }
        }
        current.SetActive(!current.activeSelf);
        temp_target.SetActive(!target.activeSelf);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    /************************************************************************************/
    #region OnClick Event => Player Menu Interaction
    public void GoBackToMainMenuScene()
    {
        current.SetActive(false);
        SceneManager.LoadScene(PREVIOUS_SCENE, LoadSceneMode.Single);
    }
    public void ExitGameApplication()
    {
        Application.Quit();
    }
    public void DisplayStatistics()
    {
        Utilities.GetCameraTransformAndRotation(GetUIWithTag(TARGET_STATS_MENU_UI), camera);
        InvertActiveUIValues(current, GetUIWithTag(TARGET_STATS_MENU_UI));
        HighScoreUI.Instance.InstanciatePlayerStatistics();
    }
    public void GetOnline()
    {
        // follow the online website of the game
    }
    public void DisplaySettings()
    {
        Utilities.GetCameraTransformAndRotation(target, camera);
        InvertActiveUIValues(current, target);
    }
    public void GoBackToInGameUI()
    {
        SetInactive(new GameObject[] { target, GetUIWithTag(TARGET_STATS_MENU_UI) });
        current.SetActive(true);
    }
    #endregion

    /************************************************************************************/

    public GameObject GetSettingsState { get => target; }

    public string GetSettingTag { get => TARGET_SETTINGS_MENU_UI; }

    #region Quick Functions to change State
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
    #endregion
}
