using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Requiered Components")]
    public static AudioMixer audioMixer;

    [Tooltip("Setting Value retrieve from MenuUI to acces Toggle state and Volume")]
    [SerializeField] private static bool isDisable;

    [Tooltip("Slider Value To Retrieve for Menu Update")]
    [SerializeField] private static float music_volume;
    [SerializeField] private static float sfx_volume;

    [Header("Slider Tag")]
    private readonly string music_slider = "Music";
    private readonly string sfx_slider = "SFX";

    private GameObject settingsUI;

    private void Start()
    {
        isDisable = false;
        settingsUI = MenuUI.Instance.GetSettingsState;
    }

    private void Update()
    {
        if (settingsUI != null)
        {
            UpdateMixerValues();
        }
    }

    /// <summary>
    /// Retrieve the Audio Mixer values both channels => Music, SFX and update its local variable
    /// so the MenuUI can retrieve the data and keep its slider values displayed Updated
    /// </summary>
    private void UpdateMixerValues()
    {
        if (settingsUI.activeSelf && settingsUI.CompareTag(MenuUI.Instance.GetSettingTag))
        {
            audioMixer.GetFloat(music_slider, out music_volume);
            audioMixer.GetFloat(sfx_slider, out sfx_volume);
        }
    }

    #region Actions
    public static void SetMainMusicVolume(Slider sliderVolume)
    {
        audioMixer.SetFloat("Music", sliderVolume.value);
    }
    public static void SetMainSFXVolume(Slider sliderVolume)
    {
        audioMixer.SetFloat("SFX", sliderVolume.value);
    }
    public static void TurnOffAudioMixer(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            isDisable = !toggle.isOn;
            audioMixer.ClearFloat("Master");
        }
        else
        {
            isDisable = toggle.isOn;
            audioMixer.SetFloat("Master", -80);
        }
    }
    #endregion

    public static bool GetMixerToggleState { get => isDisable; }

    public static float GetMusicVolume { get => music_volume; }

    public static float GetSFXVolume { get => sfx_volume; }
}
