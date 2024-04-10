using UnityEngine;

namespace Managers
{
    public class ManagerConfiguration : SingletonMonoBehaviour<ManagerConfiguration>
    {
        #region Private Attributes
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private StatsManager _statsManager;
        #endregion

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();

            ConfigureManagers();
        }

        #endregion

        #region Methods

        private void ConfigureManagers()
        {
            _statsManager.ConfigureManager();
            _uiManager.ConfigureManager();
        }
        
        #endregion
    }
}
