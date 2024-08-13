using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class ShopPanel : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private GameObject _shopPanel;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _shopPanel.SetActive(false);
        }

        private void Update()
        {
            if(!Keyboard.current.escapeKey.wasPressedThisFrame || !_shopPanel.activeSelf)
            {
                return;
            }

            HideShopPanel();
        }
        
        #endregion

        #region Methods

        public void ToggleShopPanel()
        {
            if(_shopPanel.activeSelf)
            {
                HideShopPanel();
                return;
            }

            ShowShopPanel();
        }

        public void ShowShopPanel()
        {
            _shopPanel.SetActive(true);
        }

        public void HideShopPanel()
        {
            _shopPanel.SetActive(false);
        }

        #endregion
    }
}