using _3C.Character.Movement;
using _3C.Character.Needs;
using _3C.Character.Widget;
using UnityEngine;
using World;

namespace _3C.Character
{
    [RequireComponent(typeof(Animator), typeof(CharacterController), typeof(PlayerWidget))]
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerNeeds)/*, typeof(SightBehavior)*/)]
    // [RequireComponent(typeof(GatherBehavior), typeof(PlayerInventory), typeof(BuildBehavior))]
    public class Player : MonoBehaviour
    {
        [Header("Player values")]
        [SerializeField] private Animator animator = null;
        [SerializeField] private PlayerWidget widget = null;
        [SerializeField] private PlayerMovement movement = null;
        [SerializeField] private PlayerNeeds needs = null;

        private bool IsValid => widget && movement && needs;
        public Animator Animator => animator;
        public PlayerWidget Widget => widget;
        public PlayerMovement Movement => movement;
        public PlayerNeeds Needs => needs;

        private void Start() => Init();

        private void Init()
        {
            if (!IsValid) return;
            
            GameWorld.Instance.OnDayChanged += widget.UpdateDay;
            GameWorld.Instance.OnHourChanged += widget.UpdateHour;
            GameWorld.Instance.OnTemperatureChanged += (_temperature) =>
            {
                widget.UpdateWorldTemperature(_temperature);
                
                float _acceptable = needs.AcceptableTemperature;
                float _playerTemperature = _temperature * 100.0f / _acceptable;
                _playerTemperature = _playerTemperature > 100.0f ? 100.0f : _playerTemperature;
                needs.Temperature.Set(_playerTemperature);
            };
            
            needs.Health.OnAmountChanged += movement.ApplySlowFactor;
            needs.Health.OnMinimalAmountReached += () => movement.SetCanMove(false);
        }
    }
}