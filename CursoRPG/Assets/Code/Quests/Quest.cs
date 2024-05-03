using Sirenix.OdinInspector;
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
        public bool IsQuestCompleted { get { return _currentObjectiveCount >= _objectiveCount; } }

        #endregion
    }
}