using UnityEngine;

namespace Managers
{
    public class ManagerConfiguration : MonoBehaviour
    {
        public static ManagerConfiguration Instance;
        
        #region Private Attributes

        [SerializeField] private UIManager _uiManager;
        [SerializeField] private StatsManager _statsManager;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ConfigureManagers();
        }

        #endregion

        #region Methods

        private void ConfigureManagers()
        {
            _statsManager.SetUpStats();
            _statsManager.ConfigureManager();
            _uiManager.ConfigureManager();
        }
        
        #endregion
    }
}
