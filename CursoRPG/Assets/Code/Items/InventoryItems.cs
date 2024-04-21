using UnityEngine;

namespace Items
{    
    public enum ItemType
    {
        Weapon, 
        Potion,
        Scroll,
        Ingredient,
        Treasure  //* Maybe change to chest
    }

    [CreateAssetMenu(fileName = "InventoryItems", menuName = "Items/InventoryItems", order = 0)]
    public class InventoryItems : ScriptableObject
    {
        #region Private Attributes
        
        [Header("Item Attributes")]
        [SerializeField] private string id;
        [SerializeField] private string _itemName;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Sprite _icon;
        [TextArea] [SerializeField] public string _description;

        [Header("Item Properties")]
        [SerializeField] private bool _isConsumable;
        [SerializeField] private  bool _isStackable;
        [SerializeField] private int _maxStackableAmount = 1;

        private int _currentStackableAmount = 1;

        #endregion

        #region Properties

        public string Id => id;
        public string ItemName => _itemName;
        public ItemType ItemType => _itemType;
        public Sprite Icon => _icon;
        public string Description => _description;
        public bool IsConsumable => _isConsumable;
        public bool IsStackable => _isStackable;
        public int MaxStackableAmount => _maxStackableAmount;
        public int CurrentStackableAmount => _currentStackableAmount;
        
        #endregion
    }

    
}