using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ManaPotion", menuName = "Items/ManaPotion", order = 2)]
    public class ManaPotionItem : InventoryItems
    {
        #region Private Attributes
        
        [Header("Mana Potion Attributes")]
        [SerializeField] private int _manaAmount;

        #endregion
    }
}