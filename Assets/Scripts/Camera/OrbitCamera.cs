using UnityEngine;

namespace Camera
{
    public class OrbitCamera : CameraDefault
    {
        [SerializeField, Header("Orbit values")]
        private float radius = 10.0f;

        [SerializeField]
        private float height = 5.0f;
        
        [SerializeField]
        private float angle = 0.0f;
        
        public override void MoveToTarget()
        {
            transform.position = ComputeTargetLocation();
        }

        protected override Vector3 ComputeTargetLocation()
        {
            UpdateAngle();
            float _x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float _y = height;
            float _z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            return new Vector3(_x, _y, _z);
        }

        public override void RotateToTarget()
        {
            transform.rotation = ComputeTargetRotation();
        }

        protected override Quaternion ComputeTargetRotation()
        {
            Vector3 _lookAt = Settings.Target.position - Settings.CurrentPosition;
            Quaternion _targetRotation = Quaternion.LookRotation(_lookAt, Settings.LookAtOffset);
        }

        private void UpdateAngle()
        {
            angle += Settings.RotateSpeed * Time.deltaTime;
            angle %= 360.0f;
        }
    }
}
