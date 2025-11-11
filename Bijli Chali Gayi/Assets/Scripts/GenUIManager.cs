using UnityEngine;
using UnityEngine.UI;

namespace GeneratorSystem
{
    public class GenUIManager : MonoBehaviour
    {
        [Header("Crosshair")]
        [SerializeField] private Image crosshair = null;

        [Header("UI Canvas Elements")]
        public GameObject inventoryCanvasUI;

        [Header("UI Image Elements")]
        [SerializeField] private Image fuelFillUI = null;

        [Header("UI Text Elements")]
        [SerializeField] private Text currentFuelText = null;
        [SerializeField] private Text maximumFuelText = null;

        public static GenUIManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void ShowInventory(bool on)
        {
            inventoryCanvasUI.SetActive(on);
        }

        public void UpdateInventoryUI(float currentFuel, float maximumFuel)
        {
            fuelFillUI.fillAmount = (currentFuel / maximumFuel);
            currentFuelText.text = currentFuel.ToString("0");
            maximumFuelText.text = maximumFuel.ToString("0");
        }

        public void HighlightCrosshair(bool on)
        {
            if (on)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
            }
        }
    }
}
