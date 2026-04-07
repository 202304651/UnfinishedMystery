using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("UI Elements")]
    public Button fullscreenButton;          // Fullscreen toggle button
    public TMP_Text fullscreenText;          // Text inside fullscreen button
    public Slider musicSlider;               // Music slider
    public Slider sfxSlider;                 // SFX slider
    public TMP_Dropdown resolutionDropdown;  // Resolution dropdown
    public Button applyButton;               // Apply button

    [Header("Audio Sources")]
    public AudioSource musicSource;          // Music audio source
    public AudioSource sfxSource;            // SFX audio source

    private bool fakeFullscreen = false;     // For Editor testing

    // Saved settings
    private float savedMusic;
    private float savedSFX;
    private int savedResolution;
    private bool savedFullscreen;

    void Start()
    {
        // Apply button disabled by default
        applyButton.interactable = false;

        // Fullscreen button
        fullscreenButton.onClick.RemoveAllListeners();
        fullscreenButton.onClick.AddListener(ToggleFullscreen);

        // Load saved settings
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

        UpdateFullscreenText();
        UpdateAudioSources();

        // Track changes in sliders and dropdown
        musicSlider.onValueChanged.AddListener(OnSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSliderChanged);
        resolutionDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void OnEnable()
    {
        // Temporarily disable listener updates
        musicSlider.onValueChanged.RemoveListener(OnSliderChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSliderChanged);
        resolutionDropdown.onValueChanged.RemoveListener(OnDropdownChanged);

        // Reset sliders, dropdown, fullscreen
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        resolutionDropdown.value = savedResolution;
        UpdateFullscreenText();
        UpdateAudioSources();

        // Force UI update before re-adding listeners
        Canvas.ForceUpdateCanvases();

        // Now disable Apply button safely
        applyButton.interactable = false;

        // Re-add the listeners
        musicSlider.onValueChanged.AddListener(OnSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSliderChanged);
        resolutionDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    // Fullscreen toggle
    void ToggleFullscreen()
    {
    bool newFullscreen = !Screen.fullScreen;

    if (newFullscreen)
    {
        // Fullscreen use current monitor resolution
        Resolution res = Screen.currentResolution;
        Screen.SetResolution(res.width, res.height, true);
    }
    else
    {
        // Windowed use the resolution selected in the dropdown
        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, false);
    }

    Screen.fullScreen = newFullscreen;
    fullscreenText.text = newFullscreen ? "ON" : "OFF";

    // Enable Apply button
    applyButton.interactable = true;

    }

    // Slider change handler
    void OnSliderChanged(float _)
    {
        applyButton.interactable = true;  // Enable Apply button
        UpdateAudioSources();              // Update audio in real-time
    }

    // Dropdown change handler
    void OnDropdownChanged(int _)
    {
        applyButton.interactable = true;  // Enable Apply button
    }

    // Update audio sources
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
        // Save all settings
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

    // Apply resolution depending on fullscreen
    if (Screen.fullScreen)
    {
        Resolution res = Screen.currentResolution;
        Screen.SetResolution(res.width, res.height, true);
    }
    else
    {
        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, false);
    }
        Debug.Log("Settings applied!");
    }

    // Return button clicked
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

    // Update fullscreen text
    void UpdateFullscreenText()
    {
#if UNITY_EDITOR
        fullscreenText.text = fakeFullscreen ? "ON" : "OFF";
#else
        fullscreenText.text = Screen.fullScreen ? "ON" : "OFF";
#endif
    }
}