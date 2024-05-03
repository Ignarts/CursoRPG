using Quests;
using UnityEngine;

namespace Managers
{
    public class ManagerConfiguration : MonoBehaviour
    {
        public static ManagerConfiguration Instance;
        
        #region Private Attributes

        [SerializeField] private UIManager _uiManager;
        [SerializeField] private QuestManager _questManager;
        [SerializeField] private PlayerQuestManager _playerQuestManager;

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
            _questManager.ConfigureManager();
        }
        
        #endregion
    }
}
