using UnityEngine;
using System;

namespace Camera
{
   [Serializable]
   public struct CameraOffset
   {
      [SerializeField, Header("Offset values")]
      private float xOffset;
      
      [SerializeField]
      private float yOffset;
      
      [SerializeField]
      private float zOffset;

      public Vector3 Offset => new Vector3(xOffset, yOffset, zOffset);
      
      public Vector3 GetLocalOffsetFromTarget(Transform _target, bool _isLookAt = false)
      {
         if (!_target) return Vector3.zero;
         Vector3 _x = _target.right * xOffset;
         Vector3 _y = _target.up * yOffset;
         Vector3 _z = _target.forward * zOffset;
         return (_isLookAt ? Vector3.zero : _target.position) + _x + _y + _z;
      }

      public Vector3 GetWorldOffsetFromTarget(Transform _target, bool _isLookAt = false)
      {
         if (!_target) return Vector3.zero;
         return (_isLookAt ? Vector3.zero : _target.position) + new Vector3(xOffset, yOffset, zOffset);
      }
   }
   
   [Serializable]
   public class CameraSettings
   {
      [SerializeField] private UnityEngine.Camera render = null;
      [SerializeField] private bool canMove = true;
      [SerializeField] private bool canRotate = true;
      [SerializeField] private float moveSpeed = 0.0f;
      [SerializeField] private float rotateSpeed = 0.0f;
      [SerializeField] private Transform target = null;
      
      public UnityEngine.Camera Render => render;
      public bool CanMove => canMove;
      public bool CanRotate => canRotate;
      public float MoveSpeed => moveSpeed;
      public float RotateSpeed => rotateSpeed;
      public Transform Target => target;
   }
}
