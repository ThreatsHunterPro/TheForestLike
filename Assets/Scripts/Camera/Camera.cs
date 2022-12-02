using System;
using UnityEngine;

namespace Camera
{
    public abstract class CameraDefault : MonoBehaviour
    {
        protected event Action onCameraUpdated = null; 

        [SerializeField, Header("Camera values")]
        private CameraSettings settings = null;

        public CameraSettings Settings => settings;
        
        private void Start() => Init();
        private void LateUpdate() => onCameraUpdated?.Invoke();
        private void OnDestroy() => Destroy();

        private void Init()
        {
            onCameraUpdated += () =>
            {
                MoveToTarget();
                RotateToTarget();
            };
        }
        private void Destroy()
        {
            onCameraUpdated = null;
        }
        private void Enable()
        {
            
        }
        private void Disable()
        {
            
        }

        public abstract void MoveToTarget();
        protected abstract Vector3 ComputeTargetLocation();
        public abstract void RotateToTarget();
        protected abstract Quaternion ComputeTargetRotation();
    }
}
