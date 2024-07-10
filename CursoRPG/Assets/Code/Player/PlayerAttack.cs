using Player.Scriptables;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private PlayerStats _playerStats;
        
        #endregion
        
        #region Properties

        public Weapon EquippedWeapon { get; private set; }
        
        #endregion

        #region Methods

        /// <summary>
        /// Equip a weapon to the player
        /// </summary>
        /// <param name="weapon"></param>
        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;

            _playerStats.AddBonusPerWeapon(weapon);

            if(weapon.WeaponType == WeaponType.Magic)
                _pooler.CreatePooler(weapon._projectilePrefab.gameObject);
        }

        /// <summary>
        /// Remove the equipped weapon
        /// </summary>
        public void RemoveEquippedWeapon()
        {
            if(EquippedWeapon == null)
                return;

            _playerStats.RemoveBonusPerWeapon(EquippedWeapon);

            if(EquippedWeapon.WeaponType == WeaponType.Magic)
                _pooler.ClearPool();

            EquippedWeapon = null;
        }
        
        #endregion
    }
}
