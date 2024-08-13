using UI;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ManaPotion", menuName = "Items/ManaPotion", order = 3)]
    public class ManaPotionItem : InventoryItems
    {
        #region Private Attributes
        
        [Header("Mana Potion Attributes")]
        [SerializeField] private int _manaAmount;

        #endregion

        #region Methods

        public override bool UseItem()
        {
            if(Inventory.Instance.PlayerMana.CurrentMana < Inventory.Instance.PlayerMana.MaxMana)
            {
                Inventory.Instance.PlayerMana.RestoreMana(_manaAmount);
                Debug.Log($"Item {_itemName} used. Grants player {_manaAmount} mana points.");
                return true;
            }

            Debug.Log("Player is already at full mana.");
            return false;
        }

        #endregion
    }
}