using UnityEngine;

namespace GeneratorSystem
{
    public class ActivateGenerator : MonoBehaviour
    {
        [Header("Array of Mesh renderer for objects we want to change the emission")]
        [SerializeField] private Renderer[] thisMaterial = null;
        private string emissionName = "_EMISSION";

        [SerializeField] private GameObject[] lights = null;

        [Header("UI Panel (Drop your Win Panel here)")]
        public GameObject winPanel;

        //This is just a sample example of a type of action you could do after the generator is filled, this simulated turning on lights
        public void PowerLights()
        {
            foreach (GameObject lightObjects in lights)
            {
                lightObjects.SetActive(true);
            }

            foreach (Renderer emissiveMaterial in thisMaterial)
            {
                emissiveMaterial.material.EnableKeyword(emissionName);
            }

            // ✅ Show win panel when generator is activated
            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Debug.Log("Generator Activated — WIN PANEL shown!");
            }
            else
            {
                Debug.LogWarning("Win Panel not assigned in ActivateGenerator script!");
            }
        }

        public void DeactivateLights()
        {
            foreach (GameObject lightObjects in lights)
            {
                lightObjects.SetActive(false);
            }

            foreach (Renderer emissiveMaterial in thisMaterial)
            {
                emissiveMaterial.material.DisableKeyword(emissionName);
            }
        }
    }
}
