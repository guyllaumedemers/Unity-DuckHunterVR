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
    [SerializeField] private Camera camera;
    private readonly string CURRENT_ACTIVE_UI = "MainMenuUI";
    private readonly string TARGET_UI = "SettingsMenuUI";

    [Header("Scene Name")]
    private readonly string TARGET_SCENE = "MainGameSceneFinal";

    [Header("Settings Menu Components")]
    [SerializeField] private Slider[] sliders;
    [SerializeField] private Toggle[] toggles;

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
        camera = GameObject.FindGameObjectWithTag(CAMERA_TAG).GetComponent<Camera>();
        DisableAllUI();
    }

    private void Start()
    {
        InitializeAllComponents();
        UpdateUIElements();
    }

    private void Update()
    {

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
        Slider music = GetSliderWithTag(MUSIC_SLIDER);
        music.value = AudioManager.Instance.GetMusicVolume;

        Slider sfx = GetSliderWithTag(SFX_SLIDER);
        sfx.value = AudioManager.Instance.GetSFXVolume;

        //Toggle disable_sound = GetToggleWithTag(DISABLE_SOUND_TOGGLE);
        //disable_sound.isOn = AudioManager.Instance.GetMixerToggleState;

        // need to update the enable gore
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
        Utilities.GetCameraTransformAndRotation(target, camera);
        current.SetActive(false);
        target.SetActive(true);
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
