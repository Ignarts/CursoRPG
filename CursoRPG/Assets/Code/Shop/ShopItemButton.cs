using TMPro;
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

        #endregion

        #region Properties

        public ShopItem ItemLoaded {get; private set;}
        
        #endregion

        #region Methods

        /// <summary>
        /// Configure the item to sell
        /// </summary>
        /// <param name="shopitemAttributes"></param>
        public void ConfigureItemToSell(ShopItem shopitemAttributes)
        {
            ItemLoaded = shopitemAttributes;
            _itemName.text = shopitemAttributes.ItemName;
            _itemIcon.sprite = shopitemAttributes.Item.Icon;
            _itemPrice.text = shopitemAttributes.ItemPrice.ToString("000");
        }

        #endregion
    }
}
