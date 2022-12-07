using System;
using UnityEngine;

namespace _3C.Character.Needs
{
    [Serializable]
    public class Need
    {
        public Action OnMinimalAmountReached;
        public Action OnAmountSufficient;
        public Action<float> OnAmountChanged;

        [SerializeField] private bool canDecrease = false;
        [SerializeField, Range(0.0f, 60.0f)] private float decreaseRate;
        [SerializeField, Range(0.0f, 100.0f)] private float decreaseValue;
        [SerializeField, Range(0.0f, 100.0f)] private float minimalAmount;
        [SerializeField, Range(0.0f, 100.0f)] private float currentAmount;

        public bool IsSufficient => currentAmount > minimalAmount;
        public float DecreaseRate => decreaseRate;

        public void SetDecreaseStatus(bool _status) => canDecrease = _status; 
        public void Decrease()
        {
            if (!canDecrease) return;
            Update(-decreaseValue);
        }

        public void Update(float _value)
        {
            currentAmount += _value;
            currentAmount = currentAmount > 100.0f ? 100.0f
                          : currentAmount < 0.0f ? 0.0f
                          : currentAmount;
            
            Analyse();
        }

        public void Set(float _newValue)
        {
            currentAmount = _newValue;
            Analyse();
        }

        private void Analyse()
        {
            OnAmountChanged?.Invoke(currentAmount);
            
            if (currentAmount <= minimalAmount)
            {
                OnMinimalAmountReached?.Invoke();
            }

            else
            {
                OnAmountSufficient?.Invoke();
            }
        }
    }
}
