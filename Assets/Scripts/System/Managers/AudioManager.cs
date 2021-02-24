using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioMixer))]
public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager instance;
    private AudioManager() { }
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;
        }
    }
    #endregion

    [Header("Requiered Components")]
    public AudioMixer audioMixer;

    [Tooltip("Setting Value retrieve from MenuUI to acces Toggle state and Volume")]
    [SerializeField] private bool isDisable;

    [Tooltip("Slider Value To Retrieve for Menu Update")]
    [SerializeField] private float music_volume;
    [SerializeField] private float sfx_volume;

    [Header("Slider Tag")]
    private readonly string music_slider = "Music";
    private readonly string sfx_slider = "SFX";

    private void Awake()
    {
        instance = this;
        InitializeChannelVolumesValues();
    }

    private void Start()
    {
        isDisable = false;
    }

    /// <summary>
    /// Initialize the local variables that are going to be return to set the displayed values of the slider
    /// when a new tab scene open => so that if we change scene from the MenuUI => to the game scene
    /// Our sliders values will be the same as the last scene
    /// </summary>
    private void InitializeChannelVolumesValues()
    {
        audioMixer.GetFloat(music_slider, out music_volume);
        audioMixer.GetFloat(sfx_slider, out sfx_volume);
    }

    #region Actions
    public void SetMainMusicVolume(Slider sliderVolume)
    {
        audioMixer.SetFloat("Music", sliderVolume.value);
    }
    public void SetMainSFXVolume(Slider sliderVolume)
    {
        audioMixer.SetFloat("SFX", sliderVolume.value);
    }
    public void TurnOffAudioMixer(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            isDisable = false;
            audioMixer.ClearFloat("Master");
        }
        else
        {
            isDisable = true;
            audioMixer.SetFloat("Master", -80);
        }
    }
    #endregion

    public bool GetMixerToggleState { get => isDisable; }

    public float GetMusicVolume { get => music_volume; }

    public float GetSFXVolume { get => sfx_volume; }
}
