using System.Collections.Generic;
using Items;
using UI.Buttons;
using UnityEngine;

namespace UI
{
    public class UIInventory : MonoBehaviour
    {
        #region Private Attributes
        
        [SerializeField] private InventorySlots _slot;
        [SerializeField] private Transform _container;

        private int _slotsNumber;
        private List<InventorySlots> _availableSlots = new List<InventorySlots>();

        #endregion

        #region Methods

        public void Configure(int slotsNumber)
        {
            _slotsNumber = slotsNumber;
            CreateSlots();
        }
        
        private void CreateSlots()
        {
            for (int i = 0; i < _slotsNumber; i++)
            {
                var newSlot = Instantiate(_slot, _container);

                newSlot.Configure(i);
                _availableSlots.Add(newSlot);
            }
        }

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
        #endregion
    }
}