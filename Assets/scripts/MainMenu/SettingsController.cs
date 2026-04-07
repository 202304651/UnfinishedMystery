using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("UI Elements")]
    public Button fullscreenButton;        // Fullscreen toggle button
    public TMP_Text fullscreenText;        // Text inside fullscreen button
    public Slider musicSlider;             // Music slider
    public Slider sfxSlider;               // SFX slider
    public TMP_Dropdown resolutionDropdown;// Resolution dropdown
    public Button applyButton;             // Apply button

    [Header("Audio Sources")]
    public AudioSource musicSource;        // Music audio source
    public AudioSource sfxSource;          // SFX audio source

    private bool fakeFullscreen = false;   // For Editor testing

    // Saved settings
    private float savedMusic;
    private float savedSFX;
    private int savedResolution;
    private bool savedFullscreen;

    void Start()
    {
        // Apply button starts disabled
        applyButton.interactable = false;

        // Fullscreen button setup
        fullscreenButton.onClick.RemoveAllListeners();
        fullscreenButton.onClick.AddListener(ToggleFullscreen);

        // Load saved values
        savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
        savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);
        savedResolution = PlayerPrefs.GetInt("ResolutionIndex", 0);
#if UNITY_EDITOR
        savedFullscreen = fakeFullscreen;
#else
        savedFullscreen = Screen.fullScreen;
#endif

        // Initialize sliders and dropdown
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        resolutionDropdown.value = savedResolution;
applyButton.interactable = true;  // when a setting changes
applyButton.interactable = false;
        UpdateFullscreenText();
        UpdateAudioSources();

        // Add listeners to track changes
        musicSlider.onValueChanged.AddListener(OnSettingChanged);
        sfxSlider.onValueChanged.AddListener(OnSettingChanged);
        resolutionDropdown.onValueChanged.AddListener(OnSettingChanged);
    }

    void ToggleFullscreen()
    {
#if UNITY_EDITOR
        fakeFullscreen = !fakeFullscreen;
        fullscreenText.text = fakeFullscreen ? "ON" : "OFF";
        if (fakeFullscreen != savedFullscreen) OnSettingChanged(0);
#else
        Screen.fullScreen = !Screen.fullScreen;
        fullscreenText.text = Screen.fullScreen ? "ON" : "OFF";
        if (Screen.fullScreen != savedFullscreen) OnSettingChanged(0);
#endif
    }

    // Called whenever a setting changes
    void OnSettingChanged(float _)
    {
        applyButton.interactable = true;  // Enable Apply button
        UpdateAudioSources();
    }

    void UpdateAudioSources()
    {
        if (musicSource != null)
            musicSource.volume = musicSlider.value;
        if (sfxSource != null)
            sfxSource.volume = sfxSlider.value;
    }

    // Apply button clicked
    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);

#if UNITY_EDITOR
        savedFullscreen = fakeFullscreen;
#else
        savedFullscreen = Screen.fullScreen;
#endif
        savedMusic = musicSlider.value;
        savedSFX = sfxSlider.value;
        savedResolution = resolutionDropdown.value;

        // Disable Apply button again
        applyButton.interactable = false;

        // Apply resolution change
        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        Debug.Log("Settings applied!");
    }

    public void ReturnToMenu()
    {
        // Reset sliders and dropdown to saved values
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        resolutionDropdown.value = savedResolution;

        UpdateFullscreenText();
        UpdateAudioSources();

        // Disable Apply button
        applyButton.interactable = false;
    }

    void UpdateFullscreenText()
    {
#if UNITY_EDITOR
        fullscreenText.text = fakeFullscreen ? "ON" : "OFF";
#else
        fullscreenText.text = Screen.fullScreen ? "ON" : "OFF";
#endif
    }
}