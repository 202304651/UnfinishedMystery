using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all main menu functionality:
/// - Scene navigation (Start Game)
/// - UI panels (Settings, Credits)
/// - Application control (Quit)
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Panel for Settings menu")]
    public GameObject settingsPanel;

    [Tooltip("Panel for Credits screen")]
    public GameObject creditsPanel;

    // =========================
    // GAME FLOW
    // =========================

    /// <summary>
    /// Loads the first gameplay scene.
    /// Make sure the scene is added in Build Settings.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // =========================
    // UI PANELS
    // =========================

    /// <summary>
    /// Opens the Settings panel.
    /// </summary>
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
        else
            Debug.LogWarning("Settings Panel is not assigned.");
    }

    /// <summary>
    /// Opens the Credits panel.
    /// </summary>
    public void OpenCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
        else
            Debug.LogWarning("Credits Panel is not assigned.");
    }

    /// <summary>
    /// Closes any given panel (used by Return button).
    /// </summary>
    /// <param name="panel">The panel to hide</param>
    public void ClosePanel(GameObject panel)
    {
        if (panel != null)
            panel.SetActive(false);
    }

    // =========================
    // APPLICATION CONTROL
    // =========================

    /// <summary>
    /// Exits the game application.
    /// Note: Will not work inside Unity Editor.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}