using System;
using Loot;
using Managers;
using Quests;
using UI;
using UnityEngine;

namespace Entities.AI
{
    public class EnemyLife : Life
    {
        #region Private Attributes

        [Header("Enemy Life Configuration")]
        [SerializeField] private EnemyLifeBar _LifeBarPrefab;
        [SerializeField] private Transform _lifeBarPosition;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AIController _aiController;
        [SerializeField] private EnemyInteraction _enemyInteraction;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyLoot _enemyLoot;

        [Header("Enemy Loot Configuration")]
        [SerializeField] private GameObject _lootTracker;

        private EnemyLifeBar _lifeBar;

        #endregion

        #region Events

        public static event Action<float> OnEnemyDefeated;
        
        #endregion

        #region MonoBehaviour Methods

        /// <summary>
        /// Awake method from MonoBehaviour
        /// </summary>
        private void Awake()
        {
            Configure();
        }

        private void Update()
        {
            if(!_lootTracker.activeSelf)
                return;
            
            if(_enemyLoot.IsAllLootClaimed())
            {
                _lootTracker.SetActive(false);
            }
        }
        
        #endregion

        #region Life Base Methods

        /// <summary>
        /// Configure the enemy life
        /// Inherit from Life
        /// </summary>
        public override void Configure()
        {
            base.Configure();

            CreateLifeBar();

            _lootTracker.SetActive(false);
        }

        /// <summary>
        /// Take damage from the enemy and update the life bar
        /// Inherit from Life
        /// </summary>
        /// <param name="damage"></param>
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            
            _lifeBar.ChangeCurrentLife(CurrentLife, MaxLife);
        }

        /// <summary>
        /// Defeat the enemy and disable the components
        /// Inherit from Life
        /// </summary>
        public override void Defeated()
        {
            base.Defeated();

            _spriteRenderer.enabled = false;
            _aiController.enabled = false;
            _enemyInteraction.DeactivateSelectedIndicators();
            _enemyMovement.enabled = false;
            _lifeBar.gameObject.SetActive(false);
            _lootTracker.SetActive(true);

            OnEnemyDefeated?.Invoke(_enemyLoot.Experience);
            ManagerConfiguration.Instance.QuestManager.AddProgress("Kill10Mobs", 1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create the life bar for the enemy
        /// </summary>
        private void CreateLifeBar()
        {
            _lifeBar = Instantiate(_LifeBarPrefab, _lifeBarPosition.position, Quaternion.identity);
            _lifeBar.transform.SetParent(_lifeBarPosition);
            _lifeBar.ChangeCurrentLife(CurrentLife, MaxLife);
        }
        
        #endregion
    }
}