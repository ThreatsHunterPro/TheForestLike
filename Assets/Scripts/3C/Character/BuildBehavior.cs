using System;
using System.Collections.Generic;
using _3C.Camera;
using UnityEngine;
using UnityEngine.EventSystems;
using _3C.Character.Statics;
using Managers;
using Resources.Crafts;

namespace _3C.Character
{
    public class BuildBehavior : MonoBehaviour
    {
        public event Action<bool> OnCraftBookStatusChanged = null;
        
        [Header("BuildBehavior values")]
        [SerializeField] private Animator animator = null;
        [SerializeField] private GameObject craftBook = null;
        [SerializeField] private CameraDefault render = null;
        [SerializeField] private Transform grid = null;
        [SerializeField] private List<Craft> allCrafts = new List<Craft>();
        [SerializeField] private CraftUI craftModel = null;

        public bool CraftBookStatus
        {
            get => craftBook && craftBook.activeSelf;
            private set
            {
                if (!craftBook) return;
                craftBook.SetActive(value);
                OnCraftBookStatusChanged?.Invoke(value);
            }
        }
        
        private void Start()
        {
            InitCraftBook();
        }

        private void Update()
        {
            if (Input.GetButtonDown(Inputs.CraftBook))
            {
                ToggleCraftBook();
            }
        }

        private void OnDestroy()
        {
            OnCraftBookStatusChanged = null;
        }

        private void InitCraftBook()
        {
            if (!grid || !craftModel) return;

            int _craftCount = allCrafts.Count;
            for (int _craftIndex = 0; _craftIndex < _craftCount; _craftIndex++)
            {
                CraftUI _craft = Instantiate(craftModel, grid);
                if (!_craft) continue;
                
                _craft.Init(allCrafts[_craftIndex].Icon, () =>
                {
                    ToggleCraftBook();
                    SpawnCraft(_craft.Craft);
                });

                // if (_craftIndex == 0)
                // {
                //     EventSystem.current.firstSelectedGameObject = _craft.gameObject;
                // }
            }
        }
        
        private void ToggleCraftBook()
        {
            if (!craftBook || !animator) return;
            
            bool _isActive = CraftBookStatus;
            
            if (!_isActive)
            {
                CraftBookStatus = true;
            }

            else
            {
                CameraManager.Instance.RestoreCurrent();
            }
            
            animator.SetBool(Animations.READ, !_isActive);
        }

        private void OpenCraftBook()
        {
            CameraManager.Instance.SetCurrentCamera(render);
        }
        
        private void CloseCraftBook()
        {
            CraftBookStatus = false;
        }
        
        private void SpawnCraft(Craft _craftModel)
        {
            print("Spawn craft !");
            Craft _craft = Instantiate(_craftModel);
            _craft.OnCraftEnded += SpawnBuild;
        }

        private void MoveCraft()
        {
            
        }

        private void PlaceCraft()
        {
            
        }
        
        private void SpawnBuild()
        {
            print("Spawn build !");
        }
    }
}
