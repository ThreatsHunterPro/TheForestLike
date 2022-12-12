using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace World
{
    [Serializable]
    public struct WorldItem
    {
        [Header("WorldItem values")]
        [SerializeField, Range(0, 10000)] private int amount;
        [SerializeField] private GameObject model;

        public int Amount => amount;
        public GameObject Model => model;
    }
    
    public class WorldGenerator : MonoBehaviour
    {
        public event Action<float> OnGenerationUpdated = null;
        public event Action OnGenerationEnded = null;

        [SerializeField, Range(0, 100)] private int tries = 1;
        [SerializeField, Range(0, 60.0f)] private float waitDuration = 1.0f;
        [SerializeField] private LayerMask worldItemLayer = new LayerMask();
        [SerializeField] private Collider surface = null;
        [SerializeField] private Transform parent = null;
        [SerializeField] private List<WorldItem> worldItems = new List<WorldItem>();

        private void Start()
        {
            StartCoroutine(SpawnItems());
        }

        private void OnDestroy()
        {
            OnGenerationUpdated = null;
            OnGenerationEnded = null;
        }

        private IEnumerator SpawnItems()
        {
            if (!parent) yield break;
            
            yield return new WaitForSeconds(waitDuration);
            int _currentIndex = 0;
            
            int _count = worldItems.Count;
            for (int _worldItemIndex = 0; _worldItemIndex < _count; _worldItemIndex++)
            {
                WorldItem _worldItem = worldItems[_worldItemIndex];
                GameObject _model = _worldItem.Model;
                if (!_model) continue;
                
                Collider _collider = _model.GetComponent<Collider>();
                
                int _worldItemCount = _worldItem.Amount;
                for (int worldItemIndex = 0; worldItemIndex < _worldItemCount; worldItemIndex++)
                {
                    Vector3 _position = GetPosition();
                    Quaternion _rotation = GetRotation();
                    int _currentTries = 0;
                    
                    if (_collider && !Physics.CheckBox(_position, _collider.bounds.extents, _rotation, worldItemLayer) && _currentTries < tries)
                    {
                        _position = GetPosition();
                        _currentTries++;
                    }
                    
                    Instantiate(_model, _position, _rotation, parent).name = _model.name;
                }

                _currentIndex++;
                OnGenerationUpdated?.Invoke(_currentIndex / (float)_count);
                yield return new WaitForSeconds(waitDuration);
            }
            
            OnGenerationEnded?.Invoke();
        }

        private Vector3 GetPosition()
        {
            Vector3 _extent = surface.bounds.extents;
            float _randomX =  Random.Range(-_extent.x, _extent.x);
            float _randomZ =  Random.Range(-_extent.z, _extent.z);

            return new Vector3(_randomX, 0.0f, _randomZ);
        }

        private Quaternion GetRotation()
        {
            float _randomPitch = Random.Range(0.0f, 380.0f);
            return Quaternion.Euler(0.0f, _randomPitch, 0.0f);
        }
    }
}