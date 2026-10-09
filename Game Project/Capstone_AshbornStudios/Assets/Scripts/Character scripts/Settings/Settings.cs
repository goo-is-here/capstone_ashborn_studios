using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("UI Refrences")]
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullscreenModeDropdown;
    public Slider FOVslider;
    public TMP_InputField FOVText;
    public Slider SensitivitySlider;

    [Header("Other Refrences")]
    public AudioMixer mixer;
    public Camera cam;

    private List<Resolution> resolutions = new List<Resolution>();

    FullScreenMode currentScreenMode = FullScreenMode.ExclusiveFullScreen;

    private void Start()
    {
        BuildResolutionDropdown();

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenModeDropdown.onValueChanged.AddListener(SetWindowMode);
        FOVslider.onValueChanged.AddListener(AdjustFOV);
        FOVText.onValueChanged.AddListener(AdjustFOV);

        LoadSettings();
    }

    void LoadSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        SetMasterVolume(masterVolume);
        masterVolumeSlider.value = masterVolume;
        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume(SFXVolume);
        sfxVolumeSlider.value = SFXVolume;
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume(musicVolume);
        musicVolumeSlider.value = musicVolume;
        int resolutionIndex = PlayerPrefs.GetInt("Resolution");
        SetResolution(resolutionIndex);
        int screenModeIndex = PlayerPrefs.GetInt("FullscreenMode");
        SetWindowMode(screenModeIndex);
        float fov = PlayerPrefs.GetFloat("FOV");
        if(fov == 0)
        {
            fov = 75;
        }
        AdjustFOV(fov);
        FOVslider.value = fov;
        FOVText.text = System.MathF.Round(fov, 1).ToString();
    }

    void BuildResolutionDropdown()
    {
        resolutions.Clear();
        List<string> options = new List<string>();

        foreach(Resolution res in Screen.resolutions)
        {
            if(resolutions.Exists(r => r.width == res.width && r.height == res.height)) continue;

            resolutions.Add(res);
        }

        resolutions.Sort((a, b) =>
        {
            int areaA = a.width * a.height;
            int areaB = b.width * b.height;

            return areaB.CompareTo(areaA);
        });
        
        foreach (Resolution res in resolutions)
        {
            options.Add(res.width + " x " + res.height);
        }




        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
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

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, currentScreenMode);
        resolutionDropdown.value = index;

        PlayerPrefs.SetInt("Resolution", index);
    }

    public void SetWindowMode(int index)
    {
        switch (index)
        {
            case 0:
                setWindowed();
                break;
            case 1:
                setBorderless();
                break;
            case 2:
                setFullscreen();
                break;

        }
        fullscreenModeDropdown.value = index;
        PlayerPrefs.SetInt("FullscreenMode", index);
    }

    void setWindowed()
    {
        currentScreenMode = FullScreenMode.Windowed;
        resolutionDropdown.interactable = true;
        int res = PlayerPrefs.GetInt("Resolution");
        SetResolution(res);

    }

    void setBorderless()
    {
        resolutionDropdown.interactable = false;
        currentScreenMode = FullScreenMode.FullScreenWindow;
        int nativeWidth = Display.main.systemWidth;
        int nativeHeight = Display.main.systemHeight;
        Screen.SetResolution(nativeWidth, nativeHeight, currentScreenMode);


    }

    void setFullscreen()
    {
        currentScreenMode = FullScreenMode.ExclusiveFullScreen;
        resolutionDropdown.interactable = true;
        int res = PlayerPrefs.GetInt("Resolution");
        SetResolution(res);

    }

    public void AdjustFOV(float newFOV)
    {
        cam.fieldOfView = newFOV;
        FOVText.text = newFOV.ToString();
        PlayerPrefs.SetFloat("FOV", newFOV);
    }

    public void AdjustFOV(string newFOV)
    {
        if (float.TryParse(newFOV, out float result))
        {
            cam.fieldOfView = result;
        }
        else
        {
            FOVText.text = PlayerPrefs.GetFloat("FOV").ToString();
        }

        FOVText.text = System.MathF.Round(result, 1).ToString();
        PlayerPrefs.SetFloat("FOV", result);
    }
}
