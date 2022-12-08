using System;
using UnityEngine;

namespace Resources.Collectibles
{
    public class GrowBehavior : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 100.0f)] private float growSpeed = 1.0f;
        Vector3 defaultScale = Vector3.zero;

        public bool IsAvailable => transform.localScale.Equals(defaultScale);
        
        private void Start()
        {
            defaultScale = transform.localScale;
        }
       private void Update() => Grow();

        private void Grow()
        {
            transform.localScale += Vector3.one * (growSpeed * Time.deltaTime);
            
            if (transform.localScale.magnitude > defaultScale.magnitude)
            {
                transform.localScale = defaultScale;
            }
        }

        public void ResetScale()
        {
            transform.localScale = Vector3.zero;
        }
    }
}
