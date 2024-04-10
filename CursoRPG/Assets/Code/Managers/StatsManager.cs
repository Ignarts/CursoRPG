using System;
using Player;
using Player.Scriptables;
using UnityEngine;

namespace Managers
{
    public class StatsManager : SingletonMonoBehaviour<StatsManager>
    {
        #region Private Attributes

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private PlayerLife _playerLife;
        [SerializeField] private PlayerMana _playerMana;
        
        #endregion

        #region Public Attributes

        public static event Action OnStatsUpdated;
        
        #endregion

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();

            SetUpStats();
            Configure();
        }
        
        #endregion

        #region Methods

        private void SetUpStats()
        {
            _playerLife.SetUpStats(_playerStats.MaxHealth);
            _playerMana.SetUpStats((int)_playerStats.MaxMana, _playerStats.ManaRegen);
            _characterMovement.SetUpStats(_playerStats.Speed);

            OnStatsUpdated?.Invoke();
        }

        private void Configure()
        {
            _playerLife.Configure();
            _playerMana.Configure(_playerLife);
        }
        
        #endregion
    }
}
