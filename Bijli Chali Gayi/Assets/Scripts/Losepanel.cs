using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [Header("🎮 Button References")]
    public Button retryButton;
    public Button exitButton;

    [Header("⏱ Timer Reference")]
    [Tooltip("Drag your CountdownTimer object here")]
    public CountdownTimer countdownTimer;

    void Start()
    {
        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetryClicked);

        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitClicked);
    }

    void Update()
    {
        // Shortcut keys
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRetryClicked();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExitClicked();
        }
    }

    // 🧊 When LosePanel shows — stop timer
    void OnEnable()
    {
        if (countdownTimer != null)
            countdownTimer.enabled = false;

        MouseManager.instance.UnlockCursor();
    }


    // 🔁 Retry logic
    void OnRetryClicked()
    {
        Debug.Log("🔁 Retry clicked — restarting scene & timer...");

        // Hide the panel first (so it's off after reload)
        gameObject.SetActive(false);

        // Restart the timer manually
        if (countdownTimer != null)
        {
            countdownTimer.enabled = true;
            countdownTimer.ResetTimer();
        }

        // Reload scene (fresh restart)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 🚪 Exit logic
    void OnExitClicked()
    {
        Debug.Log("🚪 Exit clicked — quitting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
