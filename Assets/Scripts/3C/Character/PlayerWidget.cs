using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using _3C.Character.Inventory;
using UnityEngine.EventSystems;

using InventoryUI = _3C.Character.Inventory.Inventory;

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
        
        [Header("Needs values")]
        [SerializeField] private Image hungerBar = null;
        [SerializeField] private Image thirstBar = null;
        [SerializeField] private Image healthBar = null;
        [SerializeField] private Image temperatureBar = null;

        [Header("Inventory values")]
        [SerializeField] private InventoryUI inventory = null;
        [SerializeField] private Transform grid = null;
        [SerializeField] private InventoryStack stackModel = null;
        [SerializeField] private List<InventoryStack> stacks = new List<InventoryStack>();

        public InventoryUI Inventory => inventory;

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

        public void UpdateInventory(List<CollectibleStack> _stacks)
        {
            if (!stackModel || !grid) return;
            
            ClearInventory();
            
            int _stackCount = _stacks.Count;
            for (int _stackIndex = 0; _stackIndex < _stackCount; _stackIndex++)
            {
                CollectibleStack _currentStack = _stacks[_stackIndex];
                
                InventoryStack _newStack = Instantiate(stackModel, grid);
                if (!_newStack) continue;
                
                _newStack.OnConsumed += _currentStack.Consume;
                _newStack.Init(_currentStack.Icon, _currentStack.Amount);
                stacks.Add(_newStack);

                if (_stackIndex == 0)
                {
                    EventSystem.current.firstSelectedGameObject = _newStack.gameObject;
                }
            }
        }

        private void ClearInventory()
        {
            for (int _childIndex = 0; _childIndex < grid.childCount; _childIndex++)
            {
                Transform _child = grid.GetChild(_childIndex);
                if (!_child) continue;
                DestroyImmediate(_child.gameObject);
            }
            
            grid.DetachChildren();
            stacks.Clear();
        }

        #endregion
    }
}
