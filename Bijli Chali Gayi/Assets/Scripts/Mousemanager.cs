using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;

    void Awake()
    {
        // Singleton setup (so other scripts can easily call it)
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Default behavior: unlocked in menus
        UnlockCursor();
    }

    // 🎮 Lock mouse during gameplay
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("🖱️ Cursor Locked (Gameplay Mode)");
    }

    // 🧾 Unlock mouse in menus / panels
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("🖱️ Cursor Unlocked (Menu / Panel Mode)");
    }
}
