using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private List<Quest> _questsAvailable;
        [Space(15)]
        [SerializeField] private UINPCQuest _uiNPCQuest;
        [SerializeField] private Transform _questContainer;
        
        #endregion

        #region Methods

        public void ConfigureManager()
        {
            LoadQuests();
        }

        private void LoadQuests()
        {
            foreach (var quest in _questsAvailable)
            {
                UINPCQuest uiNPCQuest = Instantiate(_uiNPCQuest, _questContainer);
                uiNPCQuest.ConfigureQuest(quest);
            }
        }
        
        #endregion
    }
}