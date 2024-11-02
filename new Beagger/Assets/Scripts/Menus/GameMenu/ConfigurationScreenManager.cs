using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationScreenManager : MonoBehaviour
{


    [Header("Volume Settings")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider masterVolumeSlider;


    [Header("Graphics Settings")]
    public Dropdown resolutionDropdown;
    public Toggle windowedModeToggle;

    [Header("PlayerPrefs Configurations")]
    public PlayerPrefsDataSaving prefsConfig; // Referência ao ScriptableObject

    private void OnEnable()
    {
        PopulateResolutionOptions();
        LoadSettings(); // Carrega valores salvos nos componentes da UI
    }

    private void PopulateResolutionOptions()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>
        {
            "800 x 600",
            "1024 x 768",
            "1280 x 720",
            "1366 x 768",
            "1600 x 900",
            "1920 x 1080"
        };

        resolutionDropdown.AddOptions(options);
    }

    public void SaveSettings()
    {
        // Salva os valores dos componentes da UI no PlayerPrefs
        PlayerPrefs.SetFloat(prefsConfig.musicVolume, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(prefsConfig.SFXVolume, sfxVolumeSlider.value);
        PlayerPrefs.SetFloat(prefsConfig.generalVolume, masterVolumeSlider.value);
        PlayerPrefs.SetInt(prefsConfig.screenWidth, resolutionDropdown.value);
        PlayerPrefs.SetInt(prefsConfig.isWindowMode, windowedModeToggle.isOn ? 1 : 0);

        ApplySettings();
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        // Carrega os valores salvos para os componentes da UI
        musicVolumeSlider.value = PlayerPrefs.GetFloat(prefsConfig.musicVolume, 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(prefsConfig.SFXVolume, 0.5f);
        masterVolumeSlider.value = PlayerPrefs.GetFloat(prefsConfig.generalVolume, 0.5f);
        resolutionDropdown.value = PlayerPrefs.GetInt(prefsConfig.screenWidth, resolutionDropdown.options.Count - 1);
        windowedModeToggle.isOn = PlayerPrefs.GetInt(prefsConfig.isWindowMode, 0) == 1;

        resolutionDropdown.RefreshShownValue(); // Atualiza o valor exibido na Dropdown
    }

    private void ApplySettings()
    {
        ApplyResolution(resolutionDropdown.value);
        Screen.fullScreen = !windowedModeToggle.isOn;

        GeneralAudioManager.Instance.SetMasterVolume(masterVolumeSlider.value);
        GeneralAudioManager.Instance.SetMusicVolume(musicVolumeSlider.value);
        GeneralAudioManager.Instance.SetSFXVolume(sfxVolumeSlider.value);
        AudioListener.volume = masterVolumeSlider.value;
    }

    private void ApplyResolution(int index)
    {
        switch (index)
        {
            case 0: Screen.SetResolution(800, 600, Screen.fullScreen); break;
            case 1: Screen.SetResolution(1024, 768, Screen.fullScreen); break;
            case 2: Screen.SetResolution(1280, 720, Screen.fullScreen); break;
            case 3: Screen.SetResolution(1366, 768, Screen.fullScreen); break;
            case 4: Screen.SetResolution(1600, 900, Screen.fullScreen); break;
            case 5: Screen.SetResolution(1920, 1080, Screen.fullScreen); break;
        }
    }
    private void Awake()
    {
        LoadSettings();
    }
}
