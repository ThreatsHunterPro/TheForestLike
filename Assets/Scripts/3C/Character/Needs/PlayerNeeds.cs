using UnityEngine;
using Managers.Timer;

namespace _3C.Character.Needs
{
    public class PlayerNeeds : MonoBehaviour
    {
        [Header("Player needs values")]
        [SerializeField] public Need hunger = new Need();
        [SerializeField] private Need thirst = new Need();
        [SerializeField] private Need temperature = new Need();
        [SerializeField] private Need health = new Need();
        [SerializeField] private Player owner = null;

        void Start() => InitNeeds();

        private void InitNeeds()
        {
            hunger.OnAmountChanged += owner.Widget.UpdateHunger;
            hunger.OnMinimalAmountReached += health.Decrease;
            // hunger.OnMinimalAmountReached += () => TimerManager.Instance.Clear(new Timer())
            TimerManager.Instance.AddLoop(hunger.Decrease, 0.0f, hunger.DecreaseRate);
            
            thirst.OnAmountChanged += owner.Widget.UpdateThirst;
            thirst.OnMinimalAmountReached += health.Decrease;
            // TimerManager.Instance.AddLoop(thirst.Decrease, 0.0f, thirst.DecreaseRate);

            temperature.OnAmountChanged += owner.Widget.UpdateTemperature;
            //world.DayNightCycle.OnTemperatureChanged += temperature.Update;
            temperature.OnMinimalAmountReached += health.Decrease;

            health.OnAmountChanged += owner.Widget.UpdateHealth;
        }
    }
}