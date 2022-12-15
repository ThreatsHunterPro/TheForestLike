using System;
using System.Collections.Generic;
using _3C.Character.Inventory;
using Resources.Collectibles;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Crafts
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class CraftUI : MonoBehaviour
    {
        [Header("CraftUI values")]
        [SerializeField] private Button button = null;
        [SerializeField] private Image image = null;
        [SerializeField] private Craft craft = null;

        public Craft Craft => craft;
        
        public void Init(Sprite _icon, Action _callback)
        {
            image.sprite = _icon;
            button.onClick.AddListener(() => _callback());
        }
    }

    public class Requirement
    {
        [Header("Requirement values")]
        [SerializeField] private CollectibleType collectibleType = new CollectibleType();
        [SerializeField] private int count = -1;

        public bool IsEmpty => count <= 0;
        public CollectibleType Type => collectibleType;
        
        public void Remove(int _amount)
        {
            count -= _amount;
        }
    }

    public class Craft : MonoBehaviour
    {
        public event Action OnCraftEnded = null;
        
        [Header("Craft values")]
        [SerializeField] private Sprite icon = null;
        [SerializeField] private List<Requirement> requirements = new List<Requirement>();
        [SerializeField] private Build build = null;

        public Sprite Icon => icon;
        public Build Build => build;

        private void OnDestroy()
        {
            OnCraftEnded = null;
        }
        
        // animer les requirements
        
        
        // update les requirements
        private void UpdateRequirements(List<CollectibleStack> _collectibles)
        {
            // tout parcourir
            int _collectiblesCount = _collectibles.Count;
            for (int _collectibleIndex = 0; _collectibleIndex < _collectiblesCount; _collectibleIndex++)
            {
                CollectibleStack _collectible = _collectibles[_collectibleIndex];
                if (_collectible == null) continue;
                
                // parcourir tous les requirements
                int _requirementsCount = requirements.Count;
                for (int _requirementIndex = 0; _requirementIndex < _requirementsCount; _requirementIndex++)
                {
                    Requirement _requirement = requirements[_requirementIndex];
                    if (_requirement == null) continue;
                    
                    if (!_collectible.IsSameCollectible(_requirement.Type)) continue;

                    // si c'est le meme collectible type, retirer la quantite au requirement
                    _requirement.Remove(_collectible.Amount);

                    if (!_requirement.IsEmpty) break;
                    
                    // si le requirement est vide, le supprimer   
                    requirements.Remove(_requirement);
                    break;
                }
            }

            if (requirements.Count <= 0)
            {
                // si il ne manque plus rien, invoke
                OnCraftEnded?.Invoke();
            }
        }
    }
}