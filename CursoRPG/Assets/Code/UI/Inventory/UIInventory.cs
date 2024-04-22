using System.Collections.Generic;
using Items;
using NUnit.Framework;
using TMPro;
using UI.Buttons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIInventory : MonoBehaviour
    {
        #region Private Attributes
        
        [Header("Inventory Configuration")]
        [SerializeField] private InventorySlots _slot;
        [SerializeField] private Transform _container;

        [Header("Item Description")]
        [SerializeField] private GameObject _itemDescriptionPanel;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemDescription;

        private int _slotsNumber;
        private List<InventorySlots> _availableSlots = new List<InventorySlots>();

        #endregion

        #region Properties
        
        public InventorySlots SelectedSlot { get; private set; }

        #endregion

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            InventorySlots.OnSlotInteraction += SlotInteraction;
            _itemDescriptionPanel.SetActive(false);
        }

        private void OnDisable() 
        {
            InventorySlots.OnSlotInteraction -= SlotInteraction;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Configure the inventory
        /// </summary>
        /// <param name="slotsNumber"></param>
        public void Configure(int slotsNumber)
        {
            _slotsNumber = slotsNumber;
            CreateSlots();
        }
        
        /// <summary>
        /// Create slots for the inventory
        /// </summary>
        private void CreateSlots()
        {
            for (int i = 0; i < _slotsNumber; i++)
            {
                var newSlot = Instantiate(_slot, _container);

                newSlot.Configure(i);
                _availableSlots.Add(newSlot);
            }
        }

        /// <summary>
        /// Show item on inventory
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <param name="index"></param>
        public void ShowItemOnInventory(InventoryItems item, int amount, int index)
        {
            if (index < 0 || index >= _availableSlots.Count || amount <= 0)
                return;

            InventorySlots slots = _availableSlots[index];

            if(item == null)
            {
                slots.TogglePanelValues(false);
                return;
            }

            slots.SetSlotValues(item, amount);
            slots.TogglePanelValues(true);
        }

        /// <summary>
        /// Slot interaction
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        private void SlotInteraction(InteractionType type, int index)
        {
            if(type != InteractionType.Click)
            {
                return;
            }

            SetItemDescription(index);
            GetSelectedSlot();
        }

        private void GetSelectedSlot()
        {
            GameObject selectedGO = EventSystem.current.currentSelectedGameObject;

            if (selectedGO == null)
                return;
            
            InventorySlots slot = selectedGO.GetComponent<InventorySlots>();

            if(slot == null)
                return;

            SelectedSlot = slot;
        }

        #endregion

        #region Events

        /// <summary>
        /// Set the selected index item description
        /// </summary>
        /// <param name="index"></param>
        private void SetItemDescription(int index)
        {
            var item = Inventory.Instance.InventoryItems[index];
            if(item == null)
            {
                _itemDescriptionPanel.SetActive(false);
                return;
            }
            
            _itemIcon.sprite = item.Icon;
            _itemName.text = item.ItemName;
            _itemDescription.text = item.Description;
            _itemDescriptionPanel.SetActive(true);
        }

        public void UseItem()
        {
            if(SelectedSlot == null)
                return;

            SelectedSlot.OnUseItem();
        }

        #endregion
    }
}