using System;
using _3C.Character.Statics;
using UnityEngine;

namespace _3C.Character.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public event Action<bool> OnActiveStatusChanged = null; 

        [Header("Inventory values")]
        [SerializeField] private Animator animator = null;
        [SerializeField] private GameObject body = null;

        public bool IsActive
        {
            get => body && body.activeSelf;
            private set
            {
                if (body)
                {
                    body.SetActive(value);
                    OnActiveStatusChanged.Invoke(value);
                }
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown(Inputs.Inventory))
            {
                SetStatus(!IsActive);
            }
        }

        private void OnDestroy()
        {
            OnActiveStatusChanged = null;
        }

        public void SetStatus(bool _status)
        {
            if (!animator) return;

            if (_status)
            {
                IsActive = true;
            }
        
            animator.SetBool(Animations.INVENTORY, _status);
        }
    
        private void Deactivate()
        {
            IsActive = false;
        }
    }
}