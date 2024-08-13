using UnityEngine;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        #region Private Attributes

        [Header("Configuration")]
        [SerializeField] private ShopItemButton _itemButtonPrefab;
        [SerializeField] private Transform _itemsContainer;

        [Header("Shop Items")]
        [SerializeField] private ShopItem[] _shopItemsAvailable;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            LoadItemsToSell();
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Load the items to sell
        /// </summary>
        private void LoadItemsToSell()
        {
            foreach (ShopItem item in _shopItemsAvailable)
            {
                ShopItemButton itemButton = Instantiate(_itemButtonPrefab, _itemsContainer);
                itemButton.ConfigureItemToSell(item);
            }
        }

        #endregion
    }
}
