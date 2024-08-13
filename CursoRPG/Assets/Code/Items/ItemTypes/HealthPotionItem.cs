using UI;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 2)]
    public class HealthPotionItem : InventoryItems
    {
        #region Private Attributes
        
        [Header("Health Potion Attributes")]
        [SerializeField] private int _healthAmount;

        #endregion

        #region Methods

        public override bool UseItem()
        {
            if(Inventory.Instance.PlayerLife.CurrentLife < Inventory.Instance.PlayerLife.MaxLife)
            {
                Inventory.Instance.PlayerLife.Heal(_healthAmount);
                Debug.Log($"Item {_itemName} used. Player healed {_healthAmount} health points.");
                return true;
            }

            Debug.Log("Player is already at full health.");
            return false;
        }

        #endregion
    }
}