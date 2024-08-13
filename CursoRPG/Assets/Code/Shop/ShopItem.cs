using System;
using Items;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class ShopItem
    {
        [SerializeField] private string _itemName;
        [SerializeField] private InventoryItems _item;
        [SerializeField] private int _itemPrice;

        public string ItemName => _itemName;
        public InventoryItems Item => _item;
        public int ItemPrice => _itemPrice;
    }
}
