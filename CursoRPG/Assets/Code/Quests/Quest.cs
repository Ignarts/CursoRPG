using System;
using Sirenix.OdinInspector;
using UnityEditor.PackageManager;
using UnityEngine;
namespace Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
    public class Quest : ScriptableObject
    {
        #region Private Attributes

        [Title("Quest Info")]
        [SerializeField] private string _questName;
        [SerializeField] private string _id;
        [SerializeField] private string _description;
        [SerializeField] private int _objectiveCount;
        [SerializeField] private bool _isActive;

        [Title("Quest Rewards")]
        [SerializeField] private int _goldReward;
        [SerializeField] private int _experienceReward;
        [SerializeField] private QuestRewardItem[] _questRewardItems;

        private int _currentObjectiveCount;
        private bool _isQuestCompleted;
        private bool _isQuestActive;

        #endregion

        #region Properties

        public string QuestName => _questName;
        public string Id => _id;
        public string Description => _description;
        public int ObjectiveCount => _objectiveCount;
        public int CurrentObjectiveCount => _currentObjectiveCount;
        public int GoldReward => _goldReward;
        public int ExperienceReward => _experienceReward;
        public QuestRewardItem[] QuestRewardItems => _questRewardItems;
        public bool IsQuestCompleted => _isQuestCompleted;
        public bool IsQuestActive => _isQuestActive;

        #endregion

        #region Events

        public static Action<Quest> OnQuestCompleted;
        public static Action<Quest> OnQuestProgress;
        
        #endregion

        #region Methods

        /// <summary>
        /// Set the quest as active
        /// </summary>
        public void AcceptQuest()
        {
            _isQuestActive = true;
        }

        /// <summary>
        /// Set the quest as active
        /// </summary>
        /// <param name="count"></param>
        public void AddObjectiveCount(int count)
        {
            _currentObjectiveCount += count;
            OnQuestProgress?.Invoke(this);
            CheckQuestCompleted();
        }

        /// <summary>
        /// Check if the quest is completed
        /// </summary>
        private void CheckQuestCompleted()
        {
            if (_currentObjectiveCount < _objectiveCount)
                return;

            _currentObjectiveCount = _objectiveCount;
            QuestCompleted();
        }

        /// <summary>
        /// Called when the quest is completed
        /// </summary>
        private void QuestCompleted()
        {
            if(_isQuestCompleted)
                return;

            _isQuestCompleted = true;
            OnQuestCompleted?.Invoke(this);
        }

        /// <summary>
        /// Reset the quest progress
        /// </summary>
        public void ResetQuest()
        {
            _currentObjectiveCount = 0;
            _isQuestCompleted = false;
        }
        
        #endregion
    }
}