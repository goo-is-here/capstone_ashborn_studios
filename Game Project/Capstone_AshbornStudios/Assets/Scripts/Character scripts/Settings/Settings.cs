using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;


    private void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        SetMasterVolume(masterVolume);
        masterVolumeSlider.value = masterVolume;
        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume(SFXVolume);
        sfxVolumeSlider.value = SFXVolume;
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume(musicVolume);
        musicVolumeSlider.value = musicVolume;
    }

    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

}
