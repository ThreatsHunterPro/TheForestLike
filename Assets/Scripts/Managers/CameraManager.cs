using System.Collections.Generic;
using _3C.Camera;
using UnityEngine;

namespace Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        private readonly Dictionary<int, CameraDefault> cameras = new Dictionary<int, CameraDefault>();
        [SerializeField] private CameraDefault defaultCamera = null;
        
        public CameraDefault CurrentCamera { get; private set; } = null;
        
        public void Add(CameraDefault _camera)
        {
            if (Exist(_camera)) return;
            cameras.Add(_camera.GetInstanceID(), _camera);
            SetCurrentCamera(_camera);
        }

        public void Remove(CameraDefault _camera)
        {
            if (!Exist(_camera)) return;
            cameras.Remove(_camera.GetInstanceID());
        }
        
        private bool Exist(CameraDefault _camera)
        {
            return cameras.ContainsValue(_camera);
        }

        public void SetCurrentCamera(CameraDefault _camera)
        {
            if (_camera && _camera.Equals(CurrentCamera)) return;
            
            if (CurrentCamera)
            {
                CurrentCamera.Disable();
            }
            
            CurrentCamera = _camera ? _camera : defaultCamera;
            CurrentCamera.Enable();
        }

        public void RestoreCurrent()
        {
            SetCurrentCamera(defaultCamera);
        }
    }
}