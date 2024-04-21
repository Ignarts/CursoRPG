using System.Collections.Generic;
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
        #endregion
    }
}