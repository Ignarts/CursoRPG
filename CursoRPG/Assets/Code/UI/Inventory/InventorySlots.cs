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
        /// Set the values of the slot
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void SetSlotValues(InventoryItems item, int amount)
        {
            _itemImage.sprite = item.Icon;
            _amountText.text = amount.ToString();
        }

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

        public void OnClick()
        {
            OnSlotInteraction?.Invoke(InteractionType.Click, _slotIndex);
        }

        public void OnUse()
        {
            OnSlotInteraction?.Invoke(InteractionType.Use, _slotIndex);
        }

        public void OnEquip()
        {
            OnSlotInteraction?.Invoke(InteractionType.Equip, _slotIndex);
        }

        public void OnRemove()
        {
            OnSlotInteraction?.Invoke(InteractionType.Remove, _slotIndex);
        }
        
        #endregion
    }
}