using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

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
        if (toggle.isOn)
        {
            audioMixer.ClearFloat("Master");
        }
        else
        {
            audioMixer.SetFloat("Master", -80);
        }
    }
}
