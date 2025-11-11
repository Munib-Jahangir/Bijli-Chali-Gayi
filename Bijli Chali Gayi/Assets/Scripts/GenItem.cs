using UnityEngine;
using UnityEngine.Events;

namespace GeneratorSystem
{
    public class GenItem : MonoBehaviour
    {
        [Header("Item Type")]
        public GeneratorItemType itemType;
        public enum GeneratorItemType { Jerrycan, Generator, FuelBarrel }

        [Header("Use Fuel Overtime")]
        public bool canBurnFuel;
        [SerializeField] private float burnRate = 1;
        private bool isGenFull;

        [Header("Rumble Settings")]
        public bool canRumble = false;
        [SerializeField] private float rumbleSpeed = 5.0f;
        [SerializeField] private float rumbleIntensity = 0.01f;
        private bool rumbling;
        private Vector3 generatorOrigin;

        [Header("Fuel Parameters")]
        [Range(0, 1000)] public float itemFuelAmount = 100.0f;
        [Range(0, 1000)] public float itemMaxFuelAmount = 500.0f;
        private float fuelRequired;

        [Header("Object Canvas Settings")]
        public bool showUI = true;
        [SerializeField] private PopoutUI _popoutUI = null;

        [SerializeField] private Sound fuelSwishSound = null;
        [SerializeField] private Sound waterPourSound = null;

        [SerializeField] private UnityEvent activateGenerator = null;
        [SerializeField] private UnityEvent deactivateGenerator = null;

        private void Awake()
        {
            UpdateItemStats(true);
            if (itemType == GeneratorItemType.Generator)
            {
                generatorOrigin = transform.parent.position;
            }
        }

        void ActivateGenerator()
        {
            rumbling = true;
            activateGenerator.Invoke();
        }

        void DeactivateGenerator()
        {
            rumbling = false;
            deactivateGenerator.Invoke();
        }

        private void Update()
        {
            RumbleGenerator();
            GeneratorFuelBurnLogic();
        }

        public void ObjectInteraction()
        {
            if (itemType == GeneratorItemType.Jerrycan)
            {
                JerrycanLogic();
            }

            else if (itemType == GeneratorItemType.Generator)
            {
                GeneratorLogic();
            }

            else if (itemType == GeneratorItemType.FuelBarrel)   
            {
                FuelBarrelLogic();
            }
        }

        void JerrycanLogic()
        {
            GenInventoryManager.instance.CollectJerrycan(true, itemFuelAmount);
            AudioFuelSwish();
            gameObject.SetActive(false);
        }

        void FuelBarrelLogic()
        {
            if (GenInventoryManager.instance.hasJerrycan)
            {
                if (itemFuelAmount > 0)
                {
                    float _localCurrentInventory = GenInventoryManager.instance.currentInvFuel;
                    float _localMaxInventory = GenInventoryManager.instance.maximumInvFuel;
                    float _localLeftToFill = _localMaxInventory - _localCurrentInventory;

                    if (itemFuelAmount >= _localLeftToFill)
                    {
                        GenInventoryManager.instance.SetFuelAmounts(true, _localLeftToFill);
                        itemFuelAmount -= _localLeftToFill;
                        AudioFuelSwish();
                    }
                    else
                    {
                        GenInventoryManager.instance.SetFuelAmounts(true, itemFuelAmount);
                        itemFuelAmount -= itemFuelAmount;
                        AudioFuelSwish();
                    }
                }
            }
        }

        void GeneratorLogic()
        {
            if (GenInventoryManager.instance.hasJerrycan)
            {
                fuelRequired = itemMaxFuelAmount - itemFuelAmount;
                float inventoryFuel = GenInventoryManager.instance.currentInvFuel;

                if (inventoryFuel > 0 && itemFuelAmount <= itemMaxFuelAmount)
                {
                    if (inventoryFuel <= fuelRequired)
                    {
                        itemFuelAmount = (itemFuelAmount + inventoryFuel);
                        inventoryFuel = 0;

                    }
                    else
                    {
                        itemFuelAmount = itemMaxFuelAmount;
                        inventoryFuel -= fuelRequired;
                    }

                    GenInventoryManager.instance.SetFuelAmounts(false, inventoryFuel);
                    AudioWaterPour();

                    if (itemFuelAmount >= itemMaxFuelAmount)
                    {
                        itemFuelAmount = itemMaxFuelAmount;
                    }
                }
                if (itemFuelAmount >= itemMaxFuelAmount)
                {
                    isGenFull = true;
                    ActivateGenerator();
                }
            }
        }

        void GeneratorFuelBurnLogic()
        {
            if (isGenFull)
            {
                if (canBurnFuel)
                {
                    if (itemFuelAmount > 0)
                    {
                        itemFuelAmount -= Time.deltaTime * burnRate;
                    }
                    else
                    {
                        DeactivateGenerator();
                        itemFuelAmount = 0;
                        isGenFull = false;
                    }
                }
            }
        }

        public void RumbleGenerator()
        {
            if (canRumble && rumbling)
            {
                transform.parent.localPosition = generatorOrigin + rumbleIntensity * new Vector3(
                Mathf.PerlinNoise(rumbleSpeed * Time.time, 1),
                Mathf.PerlinNoise(rumbleSpeed * Time.time, 2),
                Mathf.PerlinNoise(rumbleSpeed * Time.time, 3));
            }
        }

        void UpdateItemStats(bool on)
        {
            if (showUI)
            {
                _popoutUI.itemNameUI.text = _popoutUI.itemName;
                _popoutUI.iconImageUI.sprite = _popoutUI.iconImage;
                _popoutUI.fuelAmountUI.text = itemFuelAmount.ToString("0");
                _popoutUI.maxFuelAmountUI.text = itemMaxFuelAmount.ToString("0");
                _popoutUI.fuelGaugeUI.fillAmount = (itemFuelAmount / itemMaxFuelAmount);
            }
        }

        public void ShowObjectStats(bool showShow)
        {
            if (showUI)
            {
                _popoutUI.itemCanvas.SetActive(showShow);
                UpdateItemStats(showUI);
            }
        }

        void AudioFuelSwish()
        {
            GenAudioManager.instance.Play(fuelSwishSound.name);
        }

        void AudioWaterPour()
        {
            GenAudioManager.instance.Play(waterPourSound.name);
        }
    }
}
