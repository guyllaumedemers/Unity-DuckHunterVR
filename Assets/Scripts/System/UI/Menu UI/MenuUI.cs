using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private GameObject target;
    private readonly string CURRENT_ACTIVE_UI = "MainMenuUI";
    private readonly string TARGET_UI = "SettingsMenuUI";

    [Header("Scene Name")]
    private readonly string TARGET_SCENE = "MainGameSceneFinal";

    [Header("Settings Menu Components")]
    [SerializeField] private Slider[] sliders;
    [SerializeField] private Toggle[] toggles;

    [Header("Slider Tag")]
    private readonly string MUSIC_SLIDER = "MusicSlider";
    private readonly string SFX_SLIDER = "SfxSlider";

    [Header("Toggle Tag")]
    private readonly string DISABLE_SOUND_TOGGLE = "DisableSound";
    private readonly string ENABLE_GORE_TOGGLE = "EnableGore";

    private void Awake()
    {
        instance = this;
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

    private void DisableAllUI()
    {
        foreach (GameObject go in menus)
        {
            go.SetActive(false);
        }
    }

    private void InitializeAllComponents()
    {
        current = GetUIWithTag(CURRENT_ACTIVE_UI);
        current.SetActive(true);
        target = GetUIWithTag(TARGET_UI);
        if (target != null)
        {
            sliders = target.GetComponentsInChildren<Slider>();
            toggles = target.GetComponentsInChildren<Toggle>();
        }
    }

    private void UpdateUIElements()
    {
        if (target.CompareTag(TARGET_UI) && target.activeSelf)
        {
            Slider music = GetSliderWithTag(MUSIC_SLIDER);
            music.value = AudioManager.GetMusicVolume;

            Slider sfx = GetSliderWithTag(SFX_SLIDER);
            sfx.value = AudioManager.GetSFXVolume;

            Toggle disable_sound = GetToggleWithTag(DISABLE_SOUND_TOGGLE);
            disable_sound.isOn = AudioManager.GetMixerToggleState;

            // need to update the enable gore
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

    /************************************************************************************/
    #region OnClick Event => Opening Menu Interaction
    public void LaunchGame()
    {
        SceneManager.LoadScene(TARGET_SCENE, LoadSceneMode.Single);
    }

    public void AccessSettings()
    {
        target.SetActive(!target.activeSelf);
        current.SetActive(!current.activeSelf);
    }

    public void GoBack()
    {
        current.SetActive(!current.activeSelf);
        target.SetActive(!target.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    /************************************************************************************/

    public GameObject GetSettingsState { get => target; }

    public string GetSettingTag { get => TARGET_UI; }
}
