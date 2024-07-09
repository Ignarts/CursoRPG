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
        [SerializeField] protected string id;
        [SerializeField] protected string _itemName;
        [SerializeField] protected ItemType _itemType;
        [SerializeField] protected Sprite _icon;
        [TextArea] [SerializeField] public string _description;

        [Header("Item Properties")]
        [SerializeField] protected bool _isConsumable;
        [SerializeField] protected  bool _isStackable;
        [SerializeField] protected int _maxStackableAmount = 1;

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

        #region Methods

        public InventoryItems InventoryItemInstance()
        {
            InventoryItems newInstance = Instantiate(this);
            return newInstance;
        }

        public void AddStackableAmount(int amount)
        {
            _currentStackableAmount += amount;
        }

        public void RemoveStackableAmount(int amount)
        {
            if(_currentStackableAmount == 0)
                return;
                
            _currentStackableAmount -= amount;

            if(_currentStackableAmount < 0)
                _currentStackableAmount = 0;
        }

        public void SetAmount(int amount)
        {
            _currentStackableAmount = amount;

            if(_currentStackableAmount > _maxStackableAmount)
                _currentStackableAmount = _maxStackableAmount;
        }

        public virtual bool UseItem()
        {
            return false;
        }

        public virtual bool EquipItem()
        {
            return false;
        }

        public virtual bool RemoveItem()
        {
            return false;
        }

        #endregion
    }

    
}