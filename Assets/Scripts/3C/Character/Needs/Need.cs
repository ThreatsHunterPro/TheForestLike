using System;
using UnityEngine;

namespace _3C.Character.Needs
{
    //TODO check for native class
    [Serializable]
    public struct Need
    {
        public event Action OnMinimalAmountReached;
        public event Action<float> OnAmountChanged;

        [SerializeField, Range(0.0f, 60.0f)] private float decreaseRate;
        [SerializeField, Range(0.0f, 100.0f)] private float decreaseValue;
        [SerializeField, Range(0.0f, 100.0f)] private float minimalAmount;
        [SerializeField, Range(0.0f, 100.0f)] private float currentAmount;

        public float DecreaseRate => decreaseRate;

        public void Decrease()
        {
            Update(-decreaseValue);
        }

        public void Update(float _value)
        {
            // if (OnMinimalAmountReached == null || OnAmountChanged == null) return;
            
            currentAmount += _value;
            Debug.Log(currentAmount);
            OnAmountChanged.Invoke(currentAmount);

            if (currentAmount <= minimalAmount)
            {
                OnMinimalAmountReached.Invoke();
            }

            currentAmount = currentAmount > 100.0f ? 100.0f
                          : currentAmount < 0.0f ? 0.0f
                          : currentAmount;
        }
    }
}
