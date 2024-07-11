using System.Collections;
using Battle;
using Entities.AI;
using Player.Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerMana _playerMana;

        [Header("Range Attack Settings")]
        [SerializeField] private float _rangeAttackSpeed;
        [Tooltip("Order: Up, Right, Down, Left")]
        [SerializeField] private Transform[] _rangeAttackPositions;
        [SerializeField] private CharacterMovement _characterMovement;

        private int _rangeAttackDirection;
        private float _nextAttackTime;
        
        #endregion
        
        #region Properties

        public Weapon EquippedWeapon { get; private set; }
        public EnemyInteraction TargetEnemy { get; private set;}
        public bool IsAttacking {get; private set;}
        
        #endregion

        #region MonoBehaviour Methods

        private void Update()
        {
            GetRangeAttackDirection();

            if(Time.time < _nextAttackTime)
                return;

            if(EquippedWeapon == null || TargetEnemy == null)
                return;

            if(Keyboard.current.spaceKey.isPressed)
            {
                if(EquippedWeapon.WeaponType == WeaponType.Magic)
                    AttackWithRangeWeapon();
                else
                    Debug.Log("Melee attack not implemented yet");
                
                _nextAttackTime = Time.time + _rangeAttackSpeed;
                StartCoroutine(SetAttackCondition());
            }
        }
        
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
                _pooler.CreatePooler(weapon.ProjectilePrefab.gameObject);
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
        /// Attack the target enemy with the magic equipped weapon
        /// </summary>
        public void AttackWithRangeWeapon()
        {
            if(EquippedWeapon == null || EquippedWeapon.WeaponType == WeaponType.Melee || TargetEnemy == null)
                return;

            if(_playerMana.CurrentMana < EquippedWeapon.ManaRequired)
            {
                Debug.Log("Not enough mana to attack");
                return;
            }

            GameObject newProjectile = _pooler.GetInstance();
            newProjectile.transform.position = _rangeAttackPositions[_rangeAttackDirection].position;

            Projectile projectile = newProjectile.GetComponent<Projectile>();
            projectile.InitProjectile(TargetEnemy);

            projectile.gameObject.SetActive(true);

            _playerMana.UseMana(EquippedWeapon.ManaRequired);
        }

        private IEnumerator SetAttackCondition()
        {
            IsAttacking = true;
            _characterMovement.PlayerAnimations.PlayAttackAnimation(_rangeAttackDirection);
            yield return new WaitForSeconds(0.3f);
            IsAttacking = false;
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

        private void GetRangeAttackDirection()
        {
            Vector2 direction = _characterMovement.MoveInput;

            if(direction.x > 0.1f)
                _rangeAttackDirection = 1;
            else if(direction.x < -0.1f)
                _rangeAttackDirection = 3;
            else if(direction.y > 0.1f)
                _rangeAttackDirection = 0;
            else if(direction.y < -0.1f)
                _rangeAttackDirection = 2;
        }
        
        #endregion
    }
}
