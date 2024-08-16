using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class CraftingPanel : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private GameObject _craftingPanel;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _craftingPanel.SetActive(false);
        }

        private void Update()
        {
            if(!Keyboard.current.escapeKey.wasPressedThisFrame || !_craftingPanel.activeSelf)
            {
                return;
            }

            HideCraftingPanel();
        }
        
        #endregion

        #region Methods

        public void ToggleCraftingPanel()
        {
            if(_craftingPanel.activeSelf)
            {
                HideCraftingPanel();
                return;
            }

            ShowCraftingPanel();
        }

        public void ShowCraftingPanel()
        {
            _craftingPanel.SetActive(true);
        }

        public void HideCraftingPanel()
        {
            _craftingPanel.SetActive(false);
        }

        #endregion
    }
}