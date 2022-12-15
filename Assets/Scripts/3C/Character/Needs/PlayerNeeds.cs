using _3C.Character.Statics;
using UnityEngine;
using Managers.Timer;
using World;

namespace _3C.Character.Needs
{
    public class PlayerNeeds : MonoBehaviour
    {
        [Header("Player needs values")]
        [SerializeField] private Need hunger = null;
        [SerializeField] private Need thirst = null;
        [SerializeField] private Need temperature = null;
        [SerializeField, Range(-50.0f, 50.0f)] private float acceptableTemperature = 5.0f;
        [SerializeField] private Need health = null;
        [SerializeField] private Player owner = null;

        public bool IsDead => !health.IsSufficient;
        public float AcceptableTemperature => acceptableTemperature;
        public Need Hunger => hunger;
        public Need Thirst => thirst;
        public Need Temperature => temperature;
        public Need Health => health;
        
        void Start() => InitNeeds();

        private void InitNeeds()
        {
            hunger.OnAmountChanged += owner.Widget.UpdateHunger;
            hunger.OnMinimalAmountReached += () => health.SetDecreaseStatus(true);
            hunger.OnAmountSufficient += CheckForHealthDecrease;
            TimerManager.Instance.AddLoop(hunger.Decrease, 0.0f, hunger.DecreaseRate);
            
            thirst.OnAmountChanged += owner.Widget.UpdateThirst;
            thirst.OnMinimalAmountReached += () => health.SetDecreaseStatus(true);
            thirst.OnAmountSufficient += CheckForHealthDecrease;
            TimerManager.Instance.AddLoop(thirst.Decrease, 0.0f, thirst.DecreaseRate);

            temperature.OnAmountChanged += owner.Widget.UpdateTemperature;
            temperature.OnMinimalAmountReached += () => health.SetDecreaseStatus(true);
            temperature.OnAmountSufficient += CheckForHealthDecrease;

            health.OnAmountChanged += owner.Widget.UpdateHealth;
            health.OnMinimalAmountReached += () =>
            {
                owner.Animator.SetTrigger(Animations.DIE);
                health.OnMinimalAmountReached = null;
            };
            TimerManager.Instance.AddLoop(health.Decrease, 0.0f, health.DecreaseRate);
        }

        private void CheckForHealthDecrease()
        {
            bool _status = !hunger.IsSufficient || !thirst.IsSufficient || !temperature.IsSufficient;
            health.SetDecreaseStatus(_status);
        }
    }
}