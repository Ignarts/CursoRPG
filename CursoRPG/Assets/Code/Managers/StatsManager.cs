using System;
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
        public CharacterMovement CharacterMovement => _characterMovement;
        public PlayerLife PlayerLife => _playerLife;
        public PlayerMana PlayerMana => _playerMana;

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
            PlayerExperience.OnExpGained += AddAvailablePoints;
        }

        private void Start() 
        {            
            OnStatsUpdated?.Invoke();
        }

        private void OnDisable()
        {
            AttributeButton.OnAttributeButtonClicked -= AddAttributePoint;
            PlayerExperience.OnExpGained -= AddAvailablePoints;
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

        private void AddAvailablePoints()
        {
            _playerStats.AddAvailablePoints();

            OnStatsUpdated?.Invoke();
        }
        
        #endregion
    }
}
