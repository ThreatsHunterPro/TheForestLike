using System;
using _3C.Camera;
using Resources.Collectibles;
using UnityEngine;

namespace _3C.Character
{
    public class SightBehavior : MonoBehaviour
    {
        public event Action<Collectible> OnCollectibleSighted = null;

        [Header("SightBehavior values")]
        [SerializeField, Range(0.0f, 5.0f)] private float checkRate = 0.5f;
        [SerializeField, Range(0.0f, 100.0f)] private float radius = 0.5f;
        [SerializeField, Range(0.0f, 50.0f)] private float range = 0.5f;
        
        private void Start()
        {
            InvokeRepeating(nameof(CheckForCollectible), 0.0f, checkRate);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
            Gizmos.DrawSphere(transform.position + transform.forward * range, radius);
        }

        private void CheckForCollectible()
        {
            bool _hasHit = Physics.SphereCast(transform.position, radius, transform.forward, out RaycastHit _result, range);
            if (_hasHit)
            {
                GameObject _object = _result.collider.gameObject;
                if (_object)
                {
                    Collectible _collectible = _object.GetComponent<Collectible>();
                    if (_collectible)
                    {
                        GrowBehavior _growBehavior = _collectible.GetComponent<GrowBehavior>();
                        if (!_growBehavior || _growBehavior.IsAvailable)
                        {
                            OnCollectibleSighted?.Invoke(_collectible);
                            return;
                        }
                    }
                }
            }
            
            OnCollectibleSighted?.Invoke(null);
        }
    }
}