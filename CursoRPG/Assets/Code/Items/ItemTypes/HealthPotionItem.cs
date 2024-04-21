using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 1)]
    public class HealthPotionItem : InventoryItems
    {
        #region Private Attributes
        
        [Header("Health Potion Attributes")]
        [SerializeField] private int _healthAmount;

        #endregion
    }
}