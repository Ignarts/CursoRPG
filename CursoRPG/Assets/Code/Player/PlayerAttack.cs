using Battle;
using Entities.AI;
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
        public EnemyInteraction TargetEnemy { get; private set;}
        
        #endregion

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            TargetSelectionManager.OnEnemySelected += EnemySelectedWithRangeWeapon;
            TargetSelectionManager.OnTargetNotSelected += EnemyNotSelectedWithRangeWeapon;
            MeleeTargetSelector.OnEnemyDetected += EnemySelectedWithMeleeWeapon;
            MeleeTargetSelector.OnEnemyLost += EnemyNotSelectedWithMeleeWeapon;
        }

        private void OnDisable()
        {
            TargetSelectionManager.OnEnemySelected -= EnemySelectedWithRangeWeapon;
            TargetSelectionManager.OnTargetNotSelected += EnemyNotSelectedWithRangeWeapon;
            MeleeTargetSelector.OnEnemyDetected -= EnemySelectedWithMeleeWeapon;
            MeleeTargetSelector.OnEnemyLost -= EnemyNotSelectedWithMeleeWeapon;
        }
        
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

        /// <summary>
        /// Attack the target enemy with the Magic equipped weapon
        /// </summary>
        /// <param name="enemy"></param>
        private void EnemySelectedWithRangeWeapon(EnemyInteraction enemy)
        {
            if(EquippedWeapon == null || EquippedWeapon.WeaponType == WeaponType.Melee || TargetEnemy == enemy)
                return;

            TargetEnemy = enemy;
            TargetEnemy.ShowRangeSelectedIndicator(true);
            Debug.Log($"Selected <color=red>{TargetEnemy.name}</color> as range target");
        }

        /// <summary>
        /// Attack the target enemy with the Melee equipped weapon
        /// </summary>
        /// <param name="enemy"></param>
        private void EnemySelectedWithMeleeWeapon(EnemyInteraction enemy)
        {
            if(EquippedWeapon == null || EquippedWeapon.WeaponType == WeaponType.Magic)
                return;

            TargetEnemy = enemy;
            TargetEnemy.ShowMeleeSelectedIndicator(true);
            Debug.Log($"Selected <color=red>{TargetEnemy.name}</color> as melee target");
        }

        /// <summary>
        /// Remove the selected enemy
        /// </summary>
        private void EnemyNotSelectedWithRangeWeapon()
        {
            if(TargetEnemy == null)
                return;

            TargetEnemy.ShowRangeSelectedIndicator(false);
            TargetEnemy = null;
            Debug.Log("No target selected");
        }

        private void EnemyNotSelectedWithMeleeWeapon()
        {
            if(TargetEnemy == null)
                return;
            
            TargetEnemy.ShowMeleeSelectedIndicator(false);
            TargetEnemy = null;
            Debug.Log("Melee target lost");
        }
        
        #endregion
    }
}
