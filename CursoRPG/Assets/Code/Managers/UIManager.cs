using Player;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : BaseManager
    {
        #region Private Attributes

        [Header("Managers")]
        [SerializeField] private PlayerLifeManager _playerLifeManager;
        [SerializeField] private PlayerManaManager _playerManaManager;
        [SerializeField] private PlayerExperienceManager _playerExperienceManager;
        [SerializeField] private PlayerStatsPanel _playerStatsPanel;
        [SerializeField] private Inventory _inventory;

        [Space(5)]
        [Header("References")]
        [SerializeField] private PlayerLife _playerLife;
        [SerializeField] private PlayerMana _playerMana;
        [SerializeField] private PlayerExperience _playerExperience;
        [SerializeField] private StatsManager _playerStats;

        #endregion

        #region MonoBehaviour Methods

        public override void ConfigureManager()
        {
            base.ConfigureManager();

            _playerLifeManager.Configure(_playerLife);
            _playerManaManager.Configure(_playerMana);
            _playerExperienceManager.Configure(_playerExperience);
            _playerStatsPanel.Configure(_playerStats.PlayerStats);
        }

        #endregion
    }
}
