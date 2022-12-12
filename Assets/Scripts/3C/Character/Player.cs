using System;
using _3C.Character.Inventory;
using _3C.Character.Needs;
using Resources.Collectibles;
using UnityEngine;
using World;

namespace _3C.Character
{
    [RequireComponent(typeof(Animator), typeof(CharacterController), typeof(PlayerWidget))]
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerNeeds), typeof(SightBehavior))]
    [RequireComponent(typeof(GatherBehavior), typeof(PlayerInventory)/*, typeof(BuildBehavior)*/)]
    public class Player : MonoBehaviour
    {
        [Header("Player values")]
        [SerializeField] private Animator animator = null;
        [SerializeField] private PlayerWidget widget = null;
        [SerializeField] private PlayerMovement movement = null;
        [SerializeField] private PlayerNeeds needs = null;
        [SerializeField] private SightBehavior sight = null;
        [SerializeField] private GatherBehavior gather = null;
        [SerializeField] private PlayerInventory playerInventory = null;
 
        private bool IsValid => widget && movement && needs;
        public Animator Animator => animator;
        public PlayerWidget Widget => widget;

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

            GameWorld.Instance.WorldGenerator.OnGenerationEnded += () =>
            {
                animator.enabled = true;
                widget.enabled = true;
                widget.SetWidgetsStatus(true);
                movement.enabled = true;
                needs.enabled = true;
                sight.enabled = true;
                gather.enabled = true;
            };
            
            widget.OnInventoryStatusChanged += (isOpen) => movement.SetCanMove(!isOpen);
            
            needs.Health.OnAmountChanged += movement.ApplySlowFactor;
            needs.Health.OnMinimalAmountReached += () => movement.SetCanMove(false);

            sight.OnCollectibleSighted += (_collectible) =>
            {
                widget.UpdateSight(_collectible ? _collectible.name : String.Empty);
                gather.SetCollectible(_collectible);
            };

            gather.OnGathering += () => movement.SetCanMove(false);
            gather.OnCollectibleGathered += (_collectible, _quantity) =>
            {
                playerInventory.AddItems(_collectible, _quantity);
                movement.SetCanMove(true);
            };

            playerInventory.OnCollectibleConsumed += (_type, quantity) =>
            {
                widget.ToggleInventoryStatus();
                UpdateNeedByType(_type, quantity);
            };
            playerInventory.OnInventoryUpdated += widget.UpdateInventory;
        }

        private void UpdateNeedByType(CollectibleType _type, float _value)
        {
            switch (_type)
            {
                case CollectibleType.Food:
                    needs.Hunger.Update(_value);
                    break;
                    
                case CollectibleType.Water:
                    needs.Thirst.Update(_value);
                    break;
            }
        }
    }
}