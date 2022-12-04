using UnityEngine;
using UnityEngine.UI;

namespace _3C.Character.Widget
{
    public class PlayerWidget : MonoBehaviour
    {
        [Header("Player widget values")]
        [SerializeField] private Image hungerBar = null;
        [SerializeField] private Image thirstBar = null;
        [SerializeField] private Image healthBar = null;
        [SerializeField] private Image temperatureBar = null;
        
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
            print("UpdateHealth");
            if (!healthBar) return;
            healthBar.fillAmount = _newHealth / 100.0f;
        }
        
        public void UpdateTemperature(float _newTemperature)
        {
            if (!temperatureBar) return;
            temperatureBar.fillAmount = _newTemperature / 100.0f;
        }
    }
}
