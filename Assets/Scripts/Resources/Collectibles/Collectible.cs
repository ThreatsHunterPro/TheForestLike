using System;
using UnityEngine;

namespace Resources.Collectibles
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectible : MonoBehaviour
    {
        public event Action OnConsumed = null;
        
        [SerializeField, Range(0.0f, 60.0f)] private float gatheringDuration = 3.0f;
        [SerializeField] private Texture icon = null;

        public float GatheringDuration => gatheringDuration;
        
        public abstract void Consume();
    }
}
