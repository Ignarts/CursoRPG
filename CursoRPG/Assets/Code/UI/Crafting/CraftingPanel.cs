using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    /// <summary>
    /// Class that represents the crafting panel in the UI
    /// </summary>
    public class CraftingPanel : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private GameObject _craftingPanel;
        [SerializeField] private GameObject _craftingInfoPanel;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            HideCraftingPanel();
            HideCraftingPanelInfo();
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

        /// <summary>
        /// Toggle the crafting panel
        /// </summary>
        public void ToggleCraftingPanel()
        {
            if(_craftingPanel.activeSelf)
            {
                HideCraftingPanel();
                return;
            }

            ShowCraftingPanel();
        }

        /// <summary>
        /// Show the crafting panel
        /// </summary>
        public void ShowCraftingPanel()
        {
            _craftingPanel.SetActive(true);
        }

        /// <summary>
        /// Hide the crafting panel
        /// </summary>
        public void HideCraftingPanel()
        {
            _craftingPanel.SetActive(false);
            HideCraftingPanelInfo();
        }

        /// <summary>
        /// Toggle the crafting panel info
        /// </summary>
        public void ToggleCraftingPanelInfo()
        {
            if(_craftingInfoPanel.activeSelf)
            {
                HideCraftingPanelInfo();
                return;
            }

            ShowCraftingPanelInfo();
        }

        /// <summary>
        /// Show the crafting panel info
        /// </summary>
        public void ShowCraftingPanelInfo()
        {
            _craftingInfoPanel.SetActive(true);
        }

        /// <summary>
        /// Hide the crafting panel info
        /// </summary>
        public void HideCraftingPanelInfo()
        {
            _craftingInfoPanel.SetActive(false);
        }

        #endregion
    }
}