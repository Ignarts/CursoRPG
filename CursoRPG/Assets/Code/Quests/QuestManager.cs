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
        
        #endregion\

        //!REMOVE THIS
        private void Update()
        {
            if(Keyboard.current.oKey.wasPressedThisFrame)
            {
                AddProgress("Kill10Mobs", 1);
            }
        }
        //!END OF REMOVE
    }
}