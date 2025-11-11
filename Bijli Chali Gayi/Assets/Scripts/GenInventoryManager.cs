using UnityEngine;

namespace GeneratorSystem
{
    public class GenInventoryManager : MonoBehaviour
    {
        [Header("Jerry can OnStart?")]
        public bool hasJerrycan;

        [HideInInspector] public float currentInvFuel = 0;
        [Header("Fuel Levels")]
        [Range(0, 100)] public float maximumInvFuel;

        [HideInInspector] public bool isInventoryOpen;

        public static GenInventoryManager instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            SetFuelAmounts(true, 0);
        }

        public void CollectJerrycan(bool shouldAdd, float fuelAmount)
        {
            hasJerrycan = true;
            SetFuelAmounts(shouldAdd, fuelAmount);
            isInventoryOpen = !isInventoryOpen;
            GenUIManager.instance.ShowInventory(true);
        }

        void Update()
        {
            if (Input.GetKeyDown(GenInputManager.instance.openInventoryKey))
            {
                if (hasJerrycan)
                {
                    isInventoryOpen = !isInventoryOpen;

                    if (isInventoryOpen)
                    {
                        GenUIManager.instance.ShowInventory(true);
                    }

                    else
                    {
                        GenUIManager.instance.ShowInventory(false);
                    }
                }
            }
        }

        public void SetFuelAmounts(bool shouldAdd, float fuelAmount)
        {
            if (shouldAdd)
            {
                if (currentInvFuel <= maximumInvFuel)
                {
                    currentInvFuel += fuelAmount;

                    if (currentInvFuel >= maximumInvFuel)
                    {
                        currentInvFuel = maximumInvFuel;
                    }
                }
                else
                {
                    currentInvFuel = maximumInvFuel;
                }
            }

            else if (!shouldAdd)
            {
                currentInvFuel = fuelAmount;
            }

            GenUIManager.instance.UpdateInventoryUI(currentInvFuel, maximumInvFuel);
        }
    }
}
