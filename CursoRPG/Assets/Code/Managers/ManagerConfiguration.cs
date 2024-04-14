using UnityEngine;

namespace Managers
{
    public class ManagerConfiguration : MonoBehaviour
    {
        public static ManagerConfiguration Instance;
        
        #region Private Attributes

        [SerializeField] private UIManager _uiManager;

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
            _uiManager.ConfigureManager();
        }
        
        #endregion
    }
}
