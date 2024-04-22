using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Items;
using System;

namespace UI.Buttons
{
    public enum InteractionType
    {
        Click,
        Use,
        Equip,
        Remove
    }
    
    public class InventorySlots : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Button _button;
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _amountPanel;
        [SerializeField] private TextMeshProUGUI _amountText;
        
        private int _slotIndex;
        
        #endregion

        #region Properties

        public int SlotIndex => _slotIndex;
        
        #endregion

        #region Events

        public static Action<InteractionType, int> OnSlotInteraction;

        #endregion

        #region Methods

        public void Configure(int slotIndex)
        {
            _slotIndex = slotIndex;
            ResetValues();
        }

        /// <summary>
        /// Select the button
        /// </summary>
        public void SelectButton()
        {
            _button.Select();
        }

        /// <summary>
        /// Set the values of the slot
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void SetSlotValues(InventoryItems item, int amount)
        {
            _itemImage.sprite = item.Icon;
            _amountText.text = amount.ToString();
        }

        /// <summary>
        /// Toggle the panel values with the current state
        /// </summary>
        /// <param name="state"></param>
        public void TogglePanelValues(bool state)
        {
            _amountPanel.SetActive(state);
            _itemImage.gameObject.SetActive(state);
        }

        /// <summary>
        /// Reset the values of the slot
        /// </summary>
        private void ResetValues()
        {
            _itemImage.sprite = null;
            _itemImage.gameObject.SetActive(false);
            _amountPanel.SetActive(false);
            _amountText.text = string.Empty;
        }

        #endregion

        #region Events
        
        /// <summary>
        /// Event to control when the item is clicked
        /// </summary>
        public void OnClickItem()
        {
            OnSlotInteraction?.Invoke(InteractionType.Click, _slotIndex);

            // move item
            if(Inventory.Instance.UIInventory.InitialIndexSlotToMove == -1)
                return;
            
            if(Inventory.Instance.UIInventory.InitialIndexSlotToMove == _slotIndex)
                return;

            Inventory.Instance.MoveItem(Inventory.Instance.UIInventory.InitialIndexSlotToMove, _slotIndex);
        }

        /// <summary>
        /// Event to control when the item is used
        /// </summary>
        public void OnUseItem()
        {
            if(Inventory.Instance.InventoryItems[_slotIndex] == null)
                return;
            
            OnSlotInteraction?.Invoke(InteractionType.Use, _slotIndex);
        }

        /// <summary>
        /// Event to control when the item is equipped
        /// </summary>
        public void OnEquipItem()
        {
            OnSlotInteraction?.Invoke(InteractionType.Equip, _slotIndex);
        }

        /// <summary>
        /// Event to control when the item is removed
        /// </summary>
        public void OnRemoveItem()
        {
            OnSlotInteraction?.Invoke(InteractionType.Remove, _slotIndex);
        }
        
        #endregion
    }
}