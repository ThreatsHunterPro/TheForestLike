using System;
using UnityEngine;

namespace Resources.Collectibles
{
    public class GrowBehavior : MonoBehaviour
    {
        [Header("GrowBehavior values")]
        [SerializeField, Range(0.0f, 100.0f)] private float growDuration = 1.0f;
        [SerializeField] private GameObject iconPanel = null;
        [SerializeField] private Collider collider = null;
        private float growSpeed = 0.0f;
        private Vector3 defaultScale = Vector3.zero;

        public bool IsAvailable => transform.localScale.Equals(defaultScale);
        
        private void Start()
        {
            defaultScale = transform.localScale;
            // v = d / t
            growSpeed = defaultScale.x / growDuration;
        }
        
       private void Update() => Grow();

       private void Grow()
        {
            transform.localScale += Vector3.one * (growSpeed * Time.deltaTime);
            
            if (transform.localScale.magnitude > defaultScale.magnitude)
            {
                transform.localScale = defaultScale;
                
                if (iconPanel)
                {
                    iconPanel.SetActive(true);
                }

                if (collider)
                {
                    collider.enabled = true;
                }
            }
        }

        public void ResetScale()
        {
            transform.localScale = Vector3.zero;
           
            if (iconPanel)
            {
                iconPanel.SetActive(false);
            }
            
            if (collider)
            {
                collider.enabled = false;
            }
        }
    }
}
