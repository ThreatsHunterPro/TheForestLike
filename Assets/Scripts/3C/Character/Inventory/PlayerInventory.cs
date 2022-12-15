using System;
using System.Collections.Generic;
using UnityEngine;
using Resources.Collectibles;

namespace _3C.Character.Inventory
{
    [Serializable]
    public class CollectibleStack
    {
        public Action OnConsume;
        public Action OnStackUpdated;
        public Action OnStackEmptied;

        [Header("Collectible stack values")]
        [SerializeField] private bool isConsumable;
        [SerializeField] private CollectibleType type;
        [SerializeField] private int amount;
        [SerializeField] private Sprite icon;

        public int Amount => amount;
        public Sprite Icon => icon;
        
        public void Consume()
        {
            if (!isConsumable) return;
            
            OnConsume.Invoke();
            Remove(1);
        }

        public void Add(int _quantity)
        {
            amount += _quantity;
            OnStackUpdated?.Invoke();
        }

        public void Remove(int _quantity)
        {
            amount -= _quantity;
            
            if (amount <= 0)
            {
                amount = 0;
                OnStackEmptied?.Invoke();
            }

            else
            {
                OnStackUpdated?.Invoke();
            }
        }
        
        public bool IsSameCollectible(CollectibleType _type)
        {
            return _type.Equals(type);
        }

        public CollectibleStack(CollectibleType _type, int _amount, Sprite _icon, bool _isConsumable, Action _onConsume = null, Action _onStackUpdated = null, Action _onStackEmptied = null)
        {
            OnConsume = _onConsume;
            OnStackUpdated = _onStackUpdated;
            OnStackEmptied = _onStackEmptied;
            
            type = _type;
            amount = _amount;
            icon = _icon;
            isConsumable = _isConsumable;
        }
    }
    
    public class PlayerInventory : MonoBehaviour
    {
        public event Action<CollectibleType, float> OnCollectibleConsumed = null; 
        public event Action<List<CollectibleStack>> OnInventoryUpdated = null;

        [Header("Player inventory values")]
        [SerializeField] private List<CollectibleStack> stacks = new List<CollectibleStack>();

        private void OnDestroy()
        {
            OnCollectibleConsumed = null;
            OnInventoryUpdated = null;
        }

        public void AddItems(Collectible _collectible, int _quantity = 1)
        {
            if (!_collectible || _quantity <= 0) return;

            if (!ExistStack(_collectible.Type, out CollectibleStack _stack))
            {
                CreateStack(_collectible, _quantity);
                OnInventoryUpdated?.Invoke(stacks);
            }

            else
            {
                AddToStack(_stack, _quantity);
            }
        }

        private void CreateStack(Collectible _collectible, int _quantity = 1)
        {
            CollectibleStack _stack = new CollectibleStack(_collectible.Type, _quantity, _collectible.Icon, _collectible.IsConsumable,
                () =>
                {
                    OnCollectibleConsumed?.Invoke(_collectible.Type, _collectible.NeedsRegenValue);
                }, () => OnInventoryUpdated?.Invoke(stacks));
            
            _stack.OnStackEmptied += () => ClearStack(_stack);
            stacks.Add(_stack);
        }
        
        private void AddToStack(CollectibleStack _stack, int _quantity)
        {
            _stack.Add(_quantity);
        }
        
        private void ClearStack(CollectibleStack _stack)
        {
            _stack.OnConsume = null;
            _stack.OnStackUpdated = null;
            _stack.OnStackEmptied = null;
            stacks.Remove(_stack);
            OnInventoryUpdated?.Invoke(stacks);
        }

        private bool ExistStack(CollectibleType _type, out CollectibleStack _stack)
        {
            int _stacksCount = stacks.Count;
            for (int _stackIndex = 0; _stackIndex < _stacksCount; _stackIndex++)
            {
                CollectibleStack _currentStack = stacks[_stackIndex];
                if (_currentStack.IsSameCollectible(_type))
                {
                    _stack = _currentStack;
                    return true;
                }
            }

            _stack = null;
            return false;
        }
    }
}