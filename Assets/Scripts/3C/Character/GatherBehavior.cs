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
        
        private void Update()
        {
            if (Input.GetButtonDown("Interact"))
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
            
            isGathering = false;
            animator.SetBool(Animations.GATHER, false);
        }

        public void SetCollectible(Collectible _collectible)
        {
            if (isGathering) return;
            collectible = _collectible;
        }
    }
}