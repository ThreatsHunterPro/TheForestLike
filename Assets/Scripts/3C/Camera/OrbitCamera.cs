using UnityEngine;

namespace _3C.Camera
{
    public class OrbitCamera : CameraDefault
    {
        [Header("Orbit values")]
        [SerializeField] private float radius = 10.0f;
        [SerializeField] private float height = 5.0f;
        private float angle;

        #region Movement

        protected override void MoveToTarget()
        {
            if (!IsValidSettings || !settings.CanMove) return;
            transform.position = ComputePosition();
        }

        protected override Vector3 ComputePosition()
        {
            UpdateAngle();
            float _x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float _y = height;
            float _z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            return new Vector3(_x, _y, _z) + settings.Offset.GetOffset(settings.Target);
        }

        private void UpdateAngle()
        {
            angle += settings.MoveSpeed * Time.deltaTime;
            angle %= 360.0f;
        }

        #endregion
        
        #region Rotation

        protected override void RotateToTarget()
        {
            if (!IsValidSettings || !settings.CanRotate) return;
            transform.rotation = ComputeRotation();
        }

        #endregion
    }
}
