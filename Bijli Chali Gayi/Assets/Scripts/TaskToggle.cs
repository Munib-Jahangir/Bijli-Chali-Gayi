using UnityEngine;
using TMPro;

public class TaskTextToggle : MonoBehaviour
{
    [Header("Drop your TMP Text here")]
    public TextMeshProUGUI tasksText;  // your text object (Find Jerry Can...)

    [Header("Optional: Which key to toggle")]
    public KeyCode toggleKey = KeyCode.T;

    private bool isVisible = false;

    void Start()
    {
        if (tasksText != null)
            tasksText.gameObject.SetActive(false); // hidden at start
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleTasks();
        }
    }

    void ToggleTasks()
    {
        if (tasksText == null)
        {
            Debug.LogWarning("TMP Text not assigned in TaskTextToggle script!");
            return;
        }

        isVisible = !isVisible;
        tasksText.gameObject.SetActive(isVisible);
    }
}
