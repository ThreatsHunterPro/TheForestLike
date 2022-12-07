using System;
using UnityEngine;

namespace World
{
    public class DayNightCycle : MonoBehaviour
    {
        public event Action<float> OnAngleChanged = null;
        public event Action OnCycleEnded = null;
        
        [Header("DayNightCycle values")]
        [SerializeField, Range(0.0f, 50.0f)] private float radius = 5.0f;
        [SerializeField, Range(0.0f, 100.0f)] private float moveSpeed = 50.0f;
        [SerializeField, Range(0.0f, 100.0f)] private float rotateSpeed = 25.0f;
        [SerializeField] private float angle = 105.0f;

        private void Update()
        {
            MoveAround();
            Rotate();
        }

        private void OnDestroy()
        {
            OnAngleChanged = null;
            OnCycleEnded = null;
        }

        private void MoveAround()
        {
            ComputeAngle();
            // +75° allow to start at 7PM
            // 12h - 5h = 7h
            // 180° - (5 * 15°) = 105° / 15° = 7h
            float _x = Mathf.Cos(Mathf.Deg2Rad * -(angle + 75.0f)) * radius;
            float _y = Mathf.Sin(Mathf.Deg2Rad * -(angle + 75.0f)) * radius;
            transform.position = new Vector3(_x, _y, 0.0f);
        }

        private void ComputeAngle()
        {
            angle += moveSpeed * Time.deltaTime;
            OnAngleChanged?.Invoke(angle);

            if (angle >= 360.0f)
            {
                angle = 0.0f;
                OnCycleEnded?.Invoke();
            }
        }
        
        private void Rotate()
        {
            Quaternion _targetRotation = Quaternion.LookRotation(-transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotateSpeed * 50.0f * Time.deltaTime);
        }
    }
}