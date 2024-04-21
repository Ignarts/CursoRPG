using UI;
using UnityEngine;

namespace Items
{
    public class PickableItem : MonoBehaviour
    {
        #region Private Attributes

        [Header("Item Attributes")]
        [SerializeField] private InventoryItems _item;
        [SerializeField] private int _amount = 1;

        #endregion

        #region MonoBehaviour Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                PickUp();
            }
        }
        
        #endregion

        #region Methods

        public void PickUp()
        {
            Inventory.Instance.AddItem(_item, _amount);
            Debug.Log($"Picking up <color=green>{_item.ItemName}</color> x{_amount}");
            Destroy(gameObject);
        }

        #endregion
    }
}