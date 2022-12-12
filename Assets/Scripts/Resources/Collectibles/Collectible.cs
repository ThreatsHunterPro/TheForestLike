using System;
using UnityEngine;

namespace Resources.Collectibles
{
    public enum CollectibleType
    {
        Wood,
        Rock,
        Food,
        Water
    }
    
    [RequireComponent(typeof(Collider))]
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private bool isConsumable = false;
        [SerializeField, Range(0, 100)] private int quantityRecoverable = 1;
        [SerializeField, Range(0.0f, 100f)] private float needsRegenValue = 20.0f;
        [SerializeField, Range(0.0f, 60.0f)] private float gatheringDuration = 3.0f;
        [SerializeField] private CollectibleType type = CollectibleType.Wood;
        [SerializeField] private Sprite icon = null;

        public bool IsConsumable => isConsumable;
        public int QuantityRecoverable => quantityRecoverable;
        public float GatheringDuration => gatheringDuration;
        public float NeedsRegenValue => needsRegenValue;
        public CollectibleType Type => type;
        public Sprite Icon => icon;
    }
}
