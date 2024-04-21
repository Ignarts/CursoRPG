using Items;
using UnityEngine;

namespace UI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        #region Private Attribtues

        [Header("Inventory Attributes")]
        [SerializeField] private UIInventory _uiInventory;

        [Header("Items")]
        [SerializeField] private InventoryItems[] _items;

        private const int SLOTS_NUMBER = 24;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }
        
        #endregion

        #region Methods

        public void Configure()
        {
            _uiInventory.Configure(SLOTS_NUMBER);
            _items = new InventoryItems[SLOTS_NUMBER];
        }
        
        #endregion
    }
}