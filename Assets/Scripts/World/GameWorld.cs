using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace World
{
    public class GameWorld : Singleton<GameWorld>
    {
        public event Action<int> OnDayChanged = null;
        public event Action<int> OnHourChanged = null;
        public event Action<float> OnTemperatureChanged = null;

        #region Components

        [Header("Game world values")]
        [SerializeField] private DayNightCycle sun = null;
        [SerializeField] private WorldGenerator generator = null;
        
        public WorldGenerator WorldGenerator => generator; 

        #endregion
        
        #region Temperature

        [Header("Temperature")]
        [SerializeField, Range(-50.0f, 50.0f)] private float temperatureMin = 5.0f;
        [SerializeField, Range(-50.0f, 50.0f)] private float temperatureMax = 35.0f;
        private int day = 1;
        private int hour = 7;
        private float temperature = 20.0f;

        private float TemperatureFactor => (temperatureMax - temperatureMin) / 12.0f;
        
        #endregion

        #region Widget

        [Header("Widget")]
        [SerializeField] private GameObject widget = null;
        [SerializeField] private Image progressBar = null;
        [SerializeField] private TMP_Text progressText = null;

        private bool IsValidWidget => progressBar != null && progressText != null;
        
        #endregion

        private void Start()
        {
            if (sun)
            {
                sun.OnCycleEnded += AddDay;
                sun.OnAngleChanged += UpdateHour;
                OnHourChanged += UpdateTemperature;
            }

            if (generator && widget)
            {
                generator.OnGenerationUpdated += UpdateWorldWidget;
                generator.OnGenerationEnded += () =>
                {
                    widget.SetActive(false);
                    sun.enabled = true;
                };
            }
        }

        private void OnDestroy()
        {
            OnDayChanged = null; 
            OnHourChanged = null;
            OnTemperatureChanged = null;
        }

        private void AddDay()
        {
            day++;
            OnDayChanged?.Invoke(day);
        }

        private void UpdateHour(float _angle)
        {
            // 15° is 1H
            int _hour = Mathf.FloorToInt(_angle / 15.0f);

            if (!hour.Equals(_hour))
            {
                hour = _hour;
                OnHourChanged?.Invoke(hour);
            }
        }
        
        private void UpdateTemperature(int _hour)
        {
            // At 12h we have the hottest temperature
            temperature = temperatureMax - Mathf.Abs(12 - hour) * TemperatureFactor;
            OnTemperatureChanged?.Invoke(temperature);
        }

        private void UpdateWorldWidget(float _progress)
        {
            if (!IsValidWidget) return;
            progressBar.fillAmount = _progress;
            progressText.text = (_progress * 100.0f).ToString();
        }
    }
}