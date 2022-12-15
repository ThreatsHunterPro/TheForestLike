using System;
using Managers;
using UnityEngine;

namespace _3C.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera), typeof(AudioListener))]
    public abstract class CameraDefault : MonoBehaviour
    {
        protected event Action onCameraUpdated = null; 

        [SerializeField, Header("Camera values")]
        protected CameraSettings settings = null;

        protected bool IsValidSettings => settings != null;
        
        private void Start() => Init();
        private void LateUpdate() => onCameraUpdated?.Invoke();
        private void OnDestroy() => Destroy();

        private void Init()
        {
            CameraManager.Instance.Add(this);

            if (!settings.IsValid)
            {
                settings.Init(GetComponent<UnityEngine.Camera>(), GetComponent<AudioListener>());
            }
            
            onCameraUpdated += () =>
            {
                MoveToTarget();
                RotateToTarget();
            };
        }
        private void Destroy()
        {
            CameraManager.Instance.Remove(this);
            onCameraUpdated = null;
        }
        public void Enable()
        {
            settings.CanMove = true;
            settings.CanRotate = true;
            settings.SetRenderStatus(true);
        }
        public void Disable()
        {
            settings.CanMove = false;
            settings.CanRotate = false;
            settings.SetRenderStatus(false);
        }

        #region Movement

        protected abstract void MoveToTarget();
        protected virtual Vector3 ComputePosition()
        {
            Vector3 _targetPosition = settings.Offset.GetOffset(settings.Target);
            return Vector3.MoveTowards(settings.CurrentPosition, _targetPosition, settings.MoveSpeed * Time.deltaTime);
        }

        #endregion

        #region Rotation

        protected abstract void RotateToTarget();
        protected Quaternion ComputeRotation()
        {
            Vector3 _direction = settings.TargetPosition - settings.CurrentPosition;
            if (_direction == Vector3.zero) return Quaternion.identity;
            Vector3 _lookAt = settings.LookAtOffset.GetLookAtOffset(settings.Target);
            Quaternion _targetRotation = Quaternion.LookRotation(_direction + _lookAt);
            return Quaternion.RotateTowards(settings.CurrentRotation, _targetRotation, settings.RotateSpeed * Time.deltaTime * 10.0f);
        }

        #endregion
    }
}
