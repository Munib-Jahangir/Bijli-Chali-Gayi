using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Header("⏱ Timer Settings")]
    [Tooltip("Timer duration in seconds (e.g. 300 = 5 minutes)")]
    public float timerDuration = 300f;

    [Header("🧾 Optional Text Components")]
    [Tooltip("Assign either Text (Legacy) or TMP_Text here")]
    public Text legacyText;
    public TMP_Text tmpText;

    [Header("❌ Lose Panel")]
    [Tooltip("Assign your hidden Lose Panel here (will be shown when timer ends)")]
    public GameObject losePanel;   // 👈 Add this in Inspector

    private float timeRemaining;
    private bool timerRunning = false;
    private bool hasEnded = false;

    void Start()
    {
        timeRemaining = timerDuration;
        timerRunning = true;
        MouseManager.instance.LockCursor();

        // Ensure Lose Panel is hidden at start
        if (losePanel != null)
            losePanel.SetActive(false);
    }

    void Update()
    {
        if (!timerRunning || hasEnded) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            timerRunning = false;
            hasEnded = true;
            UpdateTimerDisplay(timeRemaining);
            OnTimerEnd();
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay);

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (tmpText != null)
            tmpText.text = formattedTime;
        else if (legacyText != null)
            legacyText.text = formattedTime;
    }

    void OnTimerEnd()
    {
        Debug.Log("⏰ Timer Finished! Showing Lose Panel...");

        // ✅ Lose panel show logic
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Lose Panel not assigned in the Inspector!");
        }
    }

    // Optional: Call to reset timer if needed
    public void ResetTimer(float newDuration = -1)
    {
        if (newDuration > 0) timerDuration = newDuration;
        timeRemaining = timerDuration;
        timerRunning = true;
        hasEnded = false;

        if (losePanel != null)
            losePanel.SetActive(false);
    }
}
