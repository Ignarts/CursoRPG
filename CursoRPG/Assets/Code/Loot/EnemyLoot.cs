using System.Collections.Generic;
using UnityEngine;

namespace Loot
{
    /// <summary>
    /// Represents the loot that an enemy can drop.
    /// </summary>
    public class EnemyLoot : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private DropItem[] _dropItemsAvailable;

        private List<DropItem> _dropItemsSelected;
        
        #endregion

        #region Properties

        public List<DropItem> DropItemsSelected => _dropItemsSelected;
        
        #endregion

        #region MonoBehavior Methods

        private void Start()
        {
            _dropItemsSelected = new List<DropItem>();
            SelectLoot();
        }
        
        #endregion

        #region Methods
        
        public void SelectLoot()
        {
            foreach (DropItem dropItem in _dropItemsAvailable)
            {
                float randomValue = Random.Range(0, 100);
                if (randomValue <= dropItem.DropChance)
                {
                    _dropItemsSelected.Add(dropItem);
                }
            }
        }

        public bool IsAllLootClaimed()
        {
            foreach (DropItem dropItem in _dropItemsSelected)
            {
                if (!dropItem.ItemPicked)
                    return false;
            }

            return true;
        }
        
        #endregion
    }
}
