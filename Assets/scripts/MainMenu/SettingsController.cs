using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("UI Elements")]
    public Button fullscreenButton;
    public TMP_Text fullscreenText;
    public Slider musicSlider;
    public Slider sfxSlider;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private bool settingsChanged = false;

    // Saved settings
    private float savedMusic;
    private float savedSFX;
    private int savedResolution;
    private bool savedFullscreen;

    private void OnEnable()
    {
        LoadSavedSettings();
        AddListeners();
        UpdateApplyButtonState();
    }

    void LoadSavedSettings()
    {
        savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
        savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);
        savedResolution = PlayerPrefs.GetInt("ResolutionIndex", 0);
        savedFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        // Set UI
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        resolutionDropdown.value = savedResolution;
        Screen.fullScreen = savedFullscreen;
        fullscreenText.text = savedFullscreen ? "ON" : "OFF";

        UpdateAudioSources();
    }

    void AddListeners()
    {
        fullscreenButton.onClick.RemoveAllListeners();
        fullscreenButton.onClick.AddListener(ToggleFullscreen);

        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener((_) => OnSettingChanged());

        sfxSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.AddListener((_) => OnSettingChanged());

        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.AddListener((_) => OnSettingChanged());

        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(ApplySettings);
    }

    void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullscreenText.text = Screen.fullScreen ? "ON" : "OFF";
        OnSettingChanged();
    }

    void OnSettingChanged()
    {
        settingsChanged = true;
        UpdateApplyButtonState();
        UpdateAudioSources();
    }

    void UpdateAudioSources()
    {
        if (musicSource) musicSource.volume = Mathf.Pow(musicSlider.value, 2); // logarithmic feel
        if (sfxSource) sfxSource.volume = Mathf.Pow(sfxSlider.value, 2);
    }

    void UpdateApplyButtonState()
    {
        applyButton.interactable = settingsChanged;
    }

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();

        // Update saved values
        savedMusic = musicSlider.value;
        savedSFX = sfxSlider.value;
        savedResolution = resolutionDropdown.value;
        savedFullscreen = Screen.fullScreen;

        // Disable apply
        settingsChanged = false;
        UpdateApplyButtonState();

        // Apply resolution
        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        Debug.Log("Settings applied!");
    }

    public void ReturnToMenu()
    {
        LoadSavedSettings();
        settingsChanged = false;
        UpdateApplyButtonState();
    }
}