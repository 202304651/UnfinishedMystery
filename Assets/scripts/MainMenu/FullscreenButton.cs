using UnityEngine;
using UnityEngine.UI;

public class FullscreenButton : MonoBehaviour
{
    public Button fullscreenButton;
    public Text buttonText;

    void Start()
    {
        // Set button text to current fullscreen state
        UpdateButtonText();

        // Add click listener
        fullscreenButton.onClick.AddListener(ToggleFullscreen);
    }

    void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        buttonText.text = Screen.fullScreen ? "ON" : "OFF";
    }
}