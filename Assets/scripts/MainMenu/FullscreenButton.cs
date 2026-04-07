using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullscreenButton : MonoBehaviour
{
    // Assign these in the Inspector
    public Button fullscreenButton;
    public TMP_Text buttonText;
    void Start()
    {
        // Set initial text based on current fullscreen state
        UpdateButtonText();

        // Add click listener
        fullscreenButton.onClick.AddListener(ToggleFullscreen);
    }

    void ToggleFullscreen()
    {
        // Switch fullscreen on/off
        Screen.fullScreen = !Screen.fullScreen;

        // Update button text
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        // Short ON/OFF text since you already have a label
        buttonText.text = Screen.fullScreen ? "ON" : "OFF";
    }
}