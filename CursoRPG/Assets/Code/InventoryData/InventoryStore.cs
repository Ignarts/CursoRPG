using Items;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "InventoryStore", menuName = "Inventory/InventoryStore")]
    public class InventoryStore : ScriptableObject
    {
        public InventoryItems[] InventoryItems;
    }
}
