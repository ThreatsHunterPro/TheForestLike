using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _3C.Character
{
    public class PlayerWidget : MonoBehaviour
    {
        [Header("Sight values")]
        [SerializeField] private GameObject collectiblePanel = null;
        [SerializeField] private TMP_Text collectible = null;
        
        [Header("World values")]
        [SerializeField] private TMP_Text day = null;
        [SerializeField] private TMP_Text hour = null;
        [SerializeField] private TMP_Text temperature = null;
        [SerializeField] private Color sufficientTemperatureColor = new Color();
        [SerializeField] private Color insufficientTemperatureColor = new Color();
        
        [Header("Stats values")]
        [SerializeField] private Image hungerBar = null;
        [SerializeField] private Image thirstBar = null;
        [SerializeField] private Image healthBar = null;
        [SerializeField] private Image temperatureBar = null;

        [Header("Stats values")]
        [SerializeField] private GameObject inventory = null;
        
        private void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                ToggleInventoryStatus();
            }
        }
        
        #region Sight

        public void UpdateSight(string _collectibleSighted)
        {
            if (!collectiblePanel || !collectible) return;

            collectible.text = _collectibleSighted;
            collectiblePanel.SetActive(!_collectibleSighted.Equals(String.Empty));
        }
        
        #endregion
        
        #region World

        public void UpdateDay(int _day)
        {
            if (!day) return;
            day.text = "Day : " + _day;
        }
        
        public void UpdateHour(int _hour)
        {
            if (!hour) return;
            hour.text = _hour + ":00";
        }
        
        public void UpdateWorldTemperature(float _temperature)
        {
            if (!temperature) return;
            temperature.text = Mathf.RoundToInt(_temperature) + "°C";
        }

        #endregion
        
        #region Stats

        public void UpdateHunger(float _newHunger)
        {
            if (!hungerBar) return;
            hungerBar.fillAmount = _newHunger / 100.0f;
        }
        
        public void UpdateThirst(float _newThirst)
        {
            if (!thirstBar) return;
            thirstBar.fillAmount = _newThirst / 100.0f;
        }
        
        public void UpdateHealth(float _newHealth)
        {
            if (!healthBar) return;
            healthBar.fillAmount = _newHealth / 100.0f;
        }
        
        public void UpdateTemperature(float _newTemperature)
        {
            if (!temperatureBar) return;
            float _temperaturePercent = _newTemperature / 100.0f;
            temperatureBar.fillAmount = _temperaturePercent;
            temperatureBar.color = _temperaturePercent < 0.5f ? insufficientTemperatureColor : sufficientTemperatureColor;
        }

        #endregion

        #region Inventory

        private void ToggleInventoryStatus()
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        public void UpdateInventory()
        {
            
        }

        #endregion
    }
}
