using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopItemButton : MonoBehaviour
    {
        #region Private Attributes

        [Header("Configuration")]
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemPrice;
        [SerializeField] private TextMeshProUGUI _amountToBuy;

        private int _amount;
        private int _startPrice;
        private int _totalPrice;

        private PlayerGoldManager _playerGoldManager;

        #endregion

        #region Properties

        public ShopItem ItemLoaded {get; private set;}
        
        #endregion

        #region Methods

        /// <summary>
        /// Configure the item to sell
        /// </summary>
        /// <param name="shopitemAttributes"></param>
        public void ConfigureItemToSell(ShopItem shopitemAttributes, PlayerGoldManager playerGoldManager)
        {
            _playerGoldManager = playerGoldManager;
            ItemLoaded = shopitemAttributes;
            _itemName.text = shopitemAttributes.ItemName;
            _itemIcon.sprite = shopitemAttributes.Item.Icon;
            _itemPrice.text = shopitemAttributes.ItemPrice.ToString("000");
            _amount = 1;
            _amountToBuy.text = _amount.ToString("00");
            _startPrice = shopitemAttributes.ItemPrice;
            _totalPrice = _startPrice;
        }

        /// <summary>
        /// Buy the item if the player has enough gold
        /// </summary>
        public void BuyItem()
        {
            if(_playerGoldManager.Gold < _totalPrice)
                return;

            Debug.Log($"Item <color=green>{ItemLoaded.ItemName}</color> bought x{_amount}");
            
            Inventory.Instance.AddItem(ItemLoaded.Item, _amount);
            GoldManager.Instance.RemoveGold(_totalPrice);
            _amount = 1;
            _totalPrice = _startPrice;

        }

        /// <summary>
        /// Update the item price
        /// </summary>
        private void UpdateItemPrice()
        {
            _amountToBuy.text = _amount.ToString("00");
            _itemPrice.text = _totalPrice.ToString("000");
        }

        /// <summary>
        /// Add the amount of items to buy. Checking if the player has enough gold
        /// </summary>
        public void AddAmount()
        {
            int price = _startPrice * (_amount + 1);

            if(_playerGoldManager.Gold < price)
                return;

            _amount++;
            _totalPrice = _startPrice * _amount;
            UpdateItemPrice();
        }

        /// <summary>
        /// Remove the amount of items to buy
        /// </summary>
        public void RemoveAmount()
        {
            if (_amount <= 1)
                return;

            _amount--;
            _totalPrice = _startPrice * _amount;
            UpdateItemPrice();

        }

        #endregion
    }
}
