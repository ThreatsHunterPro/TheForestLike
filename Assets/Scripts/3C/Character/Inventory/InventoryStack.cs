using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _3C.Character.Inventory
{
    public class InventoryStack : MonoBehaviour
    {
        public event Action OnConsumed = null; 

        [SerializeField] private Button button = null;
        [SerializeField] private Image icon = null;
        [SerializeField] private TMP_Text amount = null;

        private void Start()
        {
            if (button)
            {
                button.onClick.AddListener(OnConsumed.Invoke);
            }
        }

        private void OnDestroy()
        {
            if (button)
            {
                button.onClick.RemoveAllListeners();
            }

            OnConsumed = null;
        }

        public void Init(Sprite _icon, int _amount)
        {
            icon.sprite = _icon;
            amount.text = _amount.ToString();
        }
    }
}