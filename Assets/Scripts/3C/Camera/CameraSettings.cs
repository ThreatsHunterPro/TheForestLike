using System;
using UnityEngine;

namespace _3C.Camera
{
   [Serializable]
   public struct CameraOffset
   {
      [SerializeField] private bool isLocal;
      [SerializeField, Range(-180.0f, 180.0f)] private float xOffset;
      [SerializeField, Range(-180.0f, 180.0f)] private float yOffset;
      [SerializeField, Range(-180.0f, 180.0f)] private float zOffset;

      #region Offset

      public Vector3 GetOffset(Transform _target)
      {
         if (!_target) return Vector3.zero;
         return isLocal ? GetLocalOffset(_target) : GetWorldOffset(_target);
      }

      private Vector3 GetLocalOffset(Transform _target)
      {
         if (!_target) return Vector3.zero;
         Vector3 _x = _target.right * xOffset;
         Vector3 _y = _target.up * yOffset;
         Vector3 _z = _target.forward * zOffset;
         return _target.position + _x + _y + _z;
      }
      
      private Vector3 GetWorldOffset(Transform _target)
      {
         if (!_target) return Vector3.zero;
         return _target.position + new Vector3(xOffset, yOffset, zOffset);
      }

      #endregion

      #region LookAtOffset

      public Vector3 GetLookAtOffset(Transform _target)
      {
         if (!_target) return Vector3.zero;
         return isLocal ? GetLocalLookAtOffset(_target) : GetWorldLookAtOffset(_target);
      }
      
      private Vector3 GetLocalLookAtOffset(Transform _target)
      {
         if (!_target) return Vector3.zero;
         Vector3 _x = _target.right * xOffset;
         Vector3 _y = _target.up * yOffset;
         Vector3 _z = _target.forward * zOffset;
         return _x + _y + _z;
      }

      private Vector3 GetWorldLookAtOffset(Transform _target)
      {
         if (!_target) return Vector3.zero;
         return new Vector3(xOffset, yOffset, zOffset);
      }
      
      #endregion
   }
   
   [Serializable]
   public class CameraSettings
   {
      [SerializeField] private bool canMove = true;
      [SerializeField] private bool canRotate = true;
      [SerializeField] private float moveSpeed = 0.0f;
      [SerializeField] private float rotateSpeed = 0.0f;
      [SerializeField] private CameraOffset offset = new CameraOffset();
      [SerializeField] private CameraOffset lookAtOffset = new CameraOffset();
      [SerializeField] private UnityEngine.Camera render = null;
      [SerializeField] private AudioListener listener = null;
      [SerializeField] private Transform target = null;

      public bool IsValid => render && listener;
      public bool CanMove
      {
         get => canMove; 
         set => canMove = value; 
      }
      public bool CanRotate
      {
         get => canRotate; 
         set => canRotate = value; 
      }
      public float MoveSpeed => moveSpeed;
      public float RotateSpeed => rotateSpeed;
      public CameraOffset Offset => offset;
      public CameraOffset LookAtOffset => lookAtOffset;
      public Vector3 CurrentPosition => render ? render.transform.position : Vector3.zero;
      public Vector3 TargetPosition => target ? target.transform.position : Vector3.zero;
      public Quaternion CurrentRotation => render ? render.transform.rotation : Quaternion.identity;
      public Quaternion TargetRotation => target ? target.transform.rotation : Quaternion.identity;
      public Transform Target => target;

      public void Init(UnityEngine.Camera _render, AudioListener _listener)
      {
         render = _render;
         listener = _listener;
      }
      public void SetRenderStatus(bool _status)
      {
         if (!render || !listener) return;
         
         render.gameObject.SetActive(_status);
         listener.enabled = _status;
      }
   }
}