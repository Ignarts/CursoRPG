using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Loot
{
    public class LootButton : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemName;

        #endregion

        #region Properties

        public DropItem DropItem { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Configures the loot item with the given drop item.
        /// </summary>
        /// <param name="dropItem"></param>
        public void ConfigureLootItem(DropItem dropItem)
        {
            DropItem = dropItem;
            _itemIcon.sprite = dropItem.Item.Icon;

            string itemText = dropItem.ItemName + " x" + dropItem.Amount;
            _itemName.text = itemText;
        }

        #endregion
    }
}
