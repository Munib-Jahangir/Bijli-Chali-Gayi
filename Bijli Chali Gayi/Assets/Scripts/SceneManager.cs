using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [Header("🎮 Scene Names (Set In Inspector)")]
    [Tooltip("Enter your Main Menu scene name (exactly as in Build Settings)")]
    public string mainMenuScene = "MainMenu";

    [Tooltip("Enter your Game scene name (exactly as in Build Settings)")]
    public string gameScene = "Game";

    [Header("⌨️ Keyboard Shortcut Settings")]
    [Tooltip("Enable key shortcut to return to main menu (default: backtick `)")]
    public bool enableShortcut = true;

    void Update()
    {
        // Backtick key (`) → Go to Main Menu (only if enabled)
        if (enableShortcut && Input.GetKeyDown(KeyCode.BackQuote))
        {
            Debug.Log("🏠 Shortcut (`) pressed — Returning to Main Menu...");
            LoadMainMenu();
        }
    }

    // 🕹️ Play Game (use on Play button)
    public void LoadGame()
    {
        Debug.Log("▶️ Loading Game Scene...");
        SceneManager.LoadScene(gameScene);
    }

    // 🏠 Return to Main Menu (use on Back button or keyboard)
    public void LoadMainMenu()
    {
        Debug.Log("🏠 Loading Main Menu Scene...");
        SceneManager.LoadScene(mainMenuScene);
        MouseManager.instance.UnlockCursor();
        SceneManager.LoadScene(mainMenuScene);
    }

    // 🚪 Quit (optional)
    public void QuitGame()
    {
        Debug.Log("🚪 Quitting Game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
