﻿using System;
using Player;
using Player.Scriptables;
using UI.Buttons;
using UnityEngine;

namespace Managers
{
    public class StatsManager : BaseManager
    {
        public static StatsManager Instance;

        #region Private Attributes

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private PlayerLife _playerLife;
        [SerializeField] private PlayerMana _playerMana;
        
        #endregion

        #region Properties

        public PlayerStats PlayerStats => _playerStats;

        #endregion

        #region Public Attributes

        public static event Action OnStatsUpdated;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _playerStats.ResetStats();
            SetUpStats();
            ConfigureManager();

            AttributeButton.OnAttributeButtonClicked += AddAttributePoint;
        }

        private void Start() 
        {            
            OnStatsUpdated?.Invoke();
        }

        #endregion

        #region Methods

        public override void ConfigureManager()
        {
            Configure();
        }
        
        public void SetUpStats()
        {
            _playerLife.SetUpStats(_playerStats.MaxHealth);
            _playerMana.SetUpStats((int)_playerStats.MaxMana, _playerStats.ManaRegeneration);
            _characterMovement.SetUpStats(_playerStats.Speed);

            OnStatsUpdated?.Invoke();
        }

        private void Configure()
        {
            _playerLife.Configure();
            _playerMana.Configure(_playerLife);
        }

        private void AddAttributePoint(AttributeType attributeType)
        {
            _playerStats.AddAttributePoint(attributeType);

            OnStatsUpdated?.Invoke();
        }
        
        #endregion
    }
}
