using System;
using _3C.Character.Statics;
using UnityEngine;
using Resources.Collectibles;

namespace _3C.Character
{
    public class GatherBehavior : MonoBehaviour
    {
        public event Action<Collectible> OnCollectibleGathered = null;

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

        private void StartGathering()
        {
            if (!collectible || !animator) return;
            
            animator.SetBool(Animations.GATHER, true);
            isGathering = true;
            Invoke(nameof(Gather), collectible.GatheringDuration);            
        }

        private void Gather()
        {
            if (!collectible) return;
            
            OnCollectibleGathered?.Invoke(collectible);
            
            GrowBehavior _growBehavior = collectible.GetComponent<GrowBehavior>();
            if (_growBehavior)
            {
                _growBehavior.ResetScale();
            }

            else
            {
                collectible.gameObject.SetActive(false);
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