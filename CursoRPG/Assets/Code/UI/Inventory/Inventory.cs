using System;
using System.Collections.Generic;
using Items;
using Player;
using UI.Buttons;
using UnityEngine;

namespace UI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        #region Private Attribtues

        [Header("Player")]
        [SerializeField] private CharacterMovement _player;

        [Header("Inventory Attributes")]
        [SerializeField] private UIInventory _uiInventory;

        [Header("Items")]
        [SerializeField] private InventoryItems[] _inventoryItems;

        private const int SLOTS_NUMBER = 24;
        
        #endregion

        #region Properties

        public InventoryItems[] InventoryItems => _inventoryItems;
        public CharacterMovement PlayerMovement => _player;
        public PlayerLife PlayerLife => _player.GetComponent<PlayerLife>();
        public PlayerMana PlayerMana => _player.GetComponent<PlayerMana>();
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            InventorySlots.OnSlotInteraction += SlotInteraction;
        }

        private void OnDisable()
        {
            InventorySlots.OnSlotInteraction -= SlotInteraction;
        }
        
        #endregion

        #region Methods

        public void Configure()
        {
            _uiInventory.Configure(SLOTS_NUMBER);
            _inventoryItems = new InventoryItems[SLOTS_NUMBER];
        }

        /// <summary>
        /// Add item to the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void AddItem(InventoryItems item, int amount)
        {
            // check if there is an item and amount is greater than 0
            if (item == null || amount <= 0)
                return;

            // check if there are more items of the same type
            List<int> indexes = VerifyStocks(item.Id);

            // check if the item is not stackable or there are no items of the same type
            if (!item.IsStackable || indexes.Count <= 0)
            {
                amount = AddNewItem(item, amount);

                return;
            }

            AddExistingItem(item, amount, indexes);
        }

        /// <summary>
        /// Add new item to the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private int AddNewItem(InventoryItems item, int amount)
        {
            if (amount > item.MaxStackableAmount)
            {
                AddItemInAvailableSlot(item, item.MaxStackableAmount);
                amount -= item.MaxStackableAmount;
                AddItem(item, amount);
            }
            else
            {
                AddItemInAvailableSlot(item, amount);
            }

            return amount;
        }

        /// <summary>
        /// Add item in the first available slot
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        private void AddItemInAvailableSlot(InventoryItems item, int amount)
        {
            for(int i = 0; i < _inventoryItems.Length; i++)
            {
                if (_inventoryItems[i] == null)
                {
                    _inventoryItems[i] = item.InventoryItemInstance();
                    _inventoryItems[i].SetAmount(amount);
                    // update the inventory slot
                    _uiInventory.ShowItemOnInventory(item, amount, i);
                    return;
                }
            }
        }

        /// <summary>
        /// Add existing item to the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <param name="indexes"></param>
        private void AddExistingItem(InventoryItems item, int amount, List<int> indexes)
        {
            // sort the indexes in ascending order based on the current stackable amount
            indexes.Sort((i1, i2) => _inventoryItems[i1].CurrentStackableAmount.CompareTo(_inventoryItems[i2].CurrentStackableAmount));
        
            // check all of the items of the same type
            for (int i = 0; i < indexes.Count && amount > 0; i++)
            {
                // calculate the amount that can be added to the current slot
                int addableAmount = Math.Min(amount, item.MaxStackableAmount - _inventoryItems[indexes[i]].CurrentStackableAmount);
        
                // add the addable amount to the current stackable amount
                _inventoryItems[indexes[i]].AddStackableAmount(addableAmount);
                amount -= addableAmount;
        
                // update the inventory slot
                _uiInventory.ShowItemOnInventory(item, _inventoryItems[indexes[i]].CurrentStackableAmount, indexes[i]);
            }
        
            // if there is still amount left, add it in a new slot
            if (amount > 0)
            {
                AddNewItem(item, amount);
            }
        }

        private List<int> VerifyStocks(string itemID)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < _inventoryItems.Length; i++)
            {
                if(_inventoryItems[i] != null && _inventoryItems[i].Id == itemID)
                {
                    indexes.Add(i);
                }
            }
            
            return indexes;
        }

        private void UseItem(int index)
        {
            if(index < 0 || index >= _inventoryItems.Length || _inventoryItems[index] == null)
                return;

            if(_inventoryItems[index].UseItem())
            {
                _inventoryItems[index].RemoveStackableAmount(1);

                if(_inventoryItems[index].CurrentStackableAmount <= 0)
                {
                    _inventoryItems[index] = null;
                    _uiInventory.ShowItemOnInventory(null, 0, index);
                }
                else
                {
                    _uiInventory.ShowItemOnInventory(_inventoryItems[index], _inventoryItems[index].CurrentStackableAmount, index);
                }
            }
        }
        
        #endregion

        #region Events

        private void SlotInteraction(InteractionType type, int index)
        {
            if(type == InteractionType.Click)
                return;
            
            switch(type)
            {
                case InteractionType.Use:
                    UseItem(index);
                    break;
                case InteractionType.Remove:
                    break;
                case InteractionType.Equip:
                    break;
            }
        }
        
        #endregion
    }
}