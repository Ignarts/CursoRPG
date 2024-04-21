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
    }
}