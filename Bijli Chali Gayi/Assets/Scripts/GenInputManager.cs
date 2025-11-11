using UnityEngine;

namespace GeneratorSystem
{
    public class GenInputManager : MonoBehaviour
    {
        [Header("Raycast Pickup Input")]
        public KeyCode interactKey;

        [Header("Inventory Inputs")]
        public KeyCode openInventoryKey;

        public static GenInputManager instance;

        /// <summary>
        /// INPUTS INSIDE - RAYCAST / INVENTORY MANAGER SCRIPTS
        /// </summary>

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
