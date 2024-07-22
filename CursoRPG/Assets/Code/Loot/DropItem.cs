using System;
using Items;
using UnityEngine;

namespace Loot
{
    /// <summary>
    /// Represents an item that can be dropped by an enemy.
    /// </summary>
    [Serializable]
    public class DropItem
    {
        public string ItemName;
        public InventoryItems Item;
        public int Amount;
        [Range(0, 100)] public float DropChance;

        public bool ItemPicked { get; set; }
    }
}
