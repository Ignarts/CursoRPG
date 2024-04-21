using UnityEngine;
namespace UI.Buttons
{
    public class InventorySlots : MonoBehaviour
    {
        #region Private Attributes

        private int _slotIndex;
        
        #endregion

        #region Properties

        public int SlotIndex => _slotIndex;
        
        #endregion

        #region Methods

        public void Configure(int slotIndex)
        {
            _slotIndex = slotIndex;
        }
        
        #endregion
    }
}