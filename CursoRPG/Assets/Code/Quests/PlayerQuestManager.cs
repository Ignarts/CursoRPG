using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Quests
{
    public class PlayerQuestManager : MonoBehaviour
    {
        public static PlayerQuestManager Instance;
        
        #region Private Attributes

        [SerializeField] private List<Quest> _questsAvailable;
        [Space(15)]
        [SerializeField] private UIPlayerQuest _uiPlayerQuest;
        [SerializeField] private Transform _questContainer;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            
            Instance = this;
        }

        #endregion

        #region Methods

        private void LoadQuests(Quest quest)
        {
            UIPlayerQuest uiPlayerQuest = Instantiate(_uiPlayerQuest, _questContainer);
            uiPlayerQuest.ConfigureQuest(quest);
        }

        public void AddQuestToActive(Quest quest)
        {
            _questsAvailable.Add(quest);

            LoadQuests(quest);
        }
        
        #endregion
    }
}