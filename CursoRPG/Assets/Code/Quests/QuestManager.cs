using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private List<Quest> _questsAvailable;
        [Space(15)]
        [SerializeField] private UINPCQuest _uiNPCQuest;
        [SerializeField] private Transform _questContainer;

        [SerializeField] private QuestCompletedPanel _questCompletedPanel;
        
        #endregion

        #region Properties

        public Quest UnclaimedQuest { get; private set; }
        
        #endregion

        #region MonoBehaviour Methods
        
        private void Awake()
        {
            foreach (var quest in _questsAvailable)
            {
                quest.ResetQuest();
            }
        }

        private void OnEnable()
        {
            Quest.OnQuestCompleted += QuestCompleted;
        }

        private void OnDisable()
        {
            Quest.OnQuestCompleted -= QuestCompleted;
        }
        
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

        public void AddProgress(string questID, int amount)
        {
            Quest quest = _questsAvailable.Find(q => q.Id == questID);
            Assert.IsNotNull(quest, $"Quest with id <color=yellow>{questID}</color> not found");

            if(quest.IsQuestCompleted)
                return;

            quest.AddObjectiveCount(amount);
            Debug.Log($"Quest <color=yellow>{quest.QuestName}</color> progress: <color=yellow>{quest.CurrentObjectiveCount}</color>/<color=yellow>{quest.ObjectiveCount}</color>");
        }

        private void QuestCompleted(Quest quest)
        {
            Assert.IsNotNull(quest, "Quest is null");
            Debug.Log($"Quest <color=yellow>{quest.QuestName}</color> completed");

            _questCompletedPanel.ConfigureQuestCompletedPanel(quest);

            UnclaimedQuest = quest;
        }
        
        #endregion
    }
}