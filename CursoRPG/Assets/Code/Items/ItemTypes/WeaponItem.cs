using Items;
using UI;
using UnityEngine;
using Weapons;
namespace Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 3)]
    public class WeaponItem : InventoryItems
    {
        #region Private Attributes
        
        [Header("Weapon Attributes")]
        [SerializeField] private Weapon _weapon;

        #endregion

        #region Properties
        
        public Weapon Weapon => _weapon;

        #endregion

        #region InventoryItems Base Methods

        /// <summary>
        /// Equip the weapon. Inherited from InventoryItems
        /// </summary>
        /// <returns></returns>
        public override bool EquipItem()
        {
            if(WeaponContainer.Instance.EquippedWeapon != null)
                return false;

            WeaponContainer.Instance.EquipWeapon(this);
            Debug.Log($"Weapon <color=green>{Weapon.name}</color> <color=blue>EQUIPPED</color>");
            return true;
        }

        public override bool RemoveItem()
        {
            if(WeaponContainer.Instance.EquippedWeapon == null)
                return false;

            WeaponContainer.Instance.RemoveWeapon();
            Debug.Log($"Weapon <color=green>{Weapon.name}</color> <color=red>REMOVED</color>");
            return true;
        }

        #endregion
    }
}
