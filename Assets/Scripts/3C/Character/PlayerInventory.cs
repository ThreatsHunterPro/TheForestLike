using System;
using System.Collections.Generic;
using UnityEngine;
using Resources.Collectibles;

namespace _3C.Character
{
    [Serializable]
    public class CollectibleStack
    {
        public event Action OnEmptied;
        
        [Header("Collectible stack values")]
        [SerializeField] private Collectible collectible;
        [SerializeField] private int count;

        public void AddElement() => count++;

        public void RemoveElement()
        {
            count--;
            if (count < 0)
            {
                count = 0;
                OnEmptied?.Invoke();
            }
        }
        public bool IsSameCollectible(Collectible _collectible)
        {
            return _collectible.Equals(collectible);
        }

        public CollectibleStack(Collectible _collectible, int _count)
        {
            OnEmptied = null;
            collectible = _collectible;
            count = _count;
        }
    }
    
    public class PlayerInventory : MonoBehaviour
    {
        public event Action<Collectible> OnInventoryUpdated = null;

        [Header("Player inventory values")]
        [SerializeField] private List<CollectibleStack> stacks = new List<CollectibleStack>();

        private void OnDestroy()
        {
            OnInventoryUpdated = null;
        }

        public void AddItem(Collectible _collectible)
        {
            if (!_collectible) return;
            
            _collectible.OnConsumed += () => RemoveItem(_collectible);

            if (!AddToStack(_collectible))
            {
                CollectibleStack _stack = new CollectibleStack(_collectible, 1);
                _stack.OnEmptied += () => RemoveItem(_collectible);
                stacks.Add(_stack);
            }
            
            OnInventoryUpdated?.Invoke(_collectible);
        }

        private bool AddToStack(Collectible _collectible)
        {
            int _stacksCount = stacks.Count;
            for (int _stackIndex = 0; _stackIndex < _stacksCount; _stackIndex++)
            {
                CollectibleStack _stack = stacks[_stackIndex];
                if (_stack.IsSameCollectible(_collectible))
                {
                    _stack.AddElement();
                    return true;
                }
            }

            return false;
        }
        
        private void RemoveItem(Collectible _collectible)
        {
            if (!_collectible) return;

            RemoveToStack(_collectible);
            OnInventoryUpdated?.Invoke(_collectible);
        }
        
        private void RemoveToStack(Collectible _collectible)
        {
            int _stacksCount = stacks.Count;
            for (int _stackIndex = 0; _stackIndex < _stacksCount; _stackIndex++)
            {
                CollectibleStack _stack = stacks[_stackIndex];
                if (_stack.IsSameCollectible(_collectible))
                {
                    _stack.RemoveElement();
                    return;
                }
            }
        }
    }
}