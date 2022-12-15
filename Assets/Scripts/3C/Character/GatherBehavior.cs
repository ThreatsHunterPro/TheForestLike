using System;
using _3C.Character.Statics;
using UnityEngine;
using Resources.Collectibles;

namespace _3C.Character
{
    public class GatherBehavior : MonoBehaviour
    {
        public event Action OnGathering = null;
        public event Action<Collectible, int> OnCollectibleGathered = null;

        [Header("Gather behavior values")]
        [SerializeField] private bool isGathering = false;
        [SerializeField] private Collectible collectible = null;
        [SerializeField] private Animator animator = null;
        private bool canGather = true;
        
        public bool IsGathering => isGathering;

        private void Update()
        {
            if (canGather && Input.GetButtonDown(Inputs.Interact))
            {
                StartGathering();
            }
        }

        private void OnDestroy()
        {
            OnGathering = null;
            OnCollectibleGathered = null;
        }

        private void StartGathering()
        {
            if (!collectible || !animator) return;
            
            isGathering = true;
            OnGathering?.Invoke();
            animator.SetBool(Animations.GATHER, true);
            Invoke(nameof(Gather), collectible.GatheringDuration);            
        }

        private void Gather()
        {
            if (!collectible) return;
            
            isGathering = false;
            animator.SetBool(Animations.GATHER, false);
            OnCollectibleGathered?.Invoke(collectible, collectible.QuantityRecoverable);
            
            GrowBehavior _growBehavior = collectible.GetComponent<GrowBehavior>();
            if (_growBehavior)
            {
                _growBehavior.ResetScale();
            }

            else
            {
                DestroyImmediate(collectible.gameObject);
            }
        }

        public void SetCollectible(Collectible _collectible)
        {
            if (isGathering) return;
            collectible = _collectible;
        }

        public void SetCanGather(bool _status)
        {
            canGather = _status;
        }
    }
}