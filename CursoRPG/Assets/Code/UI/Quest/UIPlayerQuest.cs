using Quests;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIPlayerQuest : UIQuest
    {
        #region Private Attributes

        [SerializeField] private UIRewards _uIRewards;
        [SerializeField] private TextMeshProUGUI _questProgressText;
        [SerializeField] private Transform _container;

        [SerializeField] private GameObject _claimButton;

        private Quest _quest;
        
        #endregion

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            if(_quest.IsQuestCompleted)
                gameObject.SetActive(false);
            
            Quest.OnQuestProgress += AddProgress;
            Quest.OnQuestCompleted += ShowClaimButton;
        }

        private void OnDisable()
        {
            Quest.OnQuestProgress -= AddProgress;
            Quest.OnQuestCompleted -= ShowClaimButton;
        }
        
        #endregion
        
        #region UIQuest Base Methods

        /// <summary>
        /// Configure the quest UI
        /// </summary>
        /// <param name="quest"></param>
        public override void ConfigureQuest(Quest quest)
        {
            base.ConfigureQuest(quest);

            _quest = quest;
            _questProgressText.text = $"{_quest.CurrentObjectiveCount}/{_quest.ObjectiveCount}";

            ConfigureRewards();

            _claimButton.SetActive(_quest.IsQuestCompleted);
        }

        /// <summary>
        /// Claim the quest rewards
        /// </summary>
        private void ConfigureRewards()
        {
            UIRewards goldRewards = Instantiate(_uIRewards, _container);
            goldRewards.ConfigureReward(_quest.GoldReward, RewardType.Gold);

            UIRewards xpRewards = Instantiate(_uIRewards, _container);
            xpRewards.ConfigureReward(_quest.ExperienceReward, RewardType.Experience);

            foreach ( var QuestRewardItem in _quest.QuestRewardItems)
            {
                UIRewards rewards = Instantiate(_uIRewards, _container);
                rewards.ConfigureReward(QuestRewardItem.Amount, QuestRewardItem.InventoryItemRewarded.Icon);
            }
        }

        /// <summary>
        /// Add progress to the quest
        /// </summary>
        /// <param name="quest"></param>
        private void AddProgress(Quest quest)
        {
            if(quest != _quest)
                return;

            _questProgressText.text = $"{quest.CurrentObjectiveCount}/{quest.ObjectiveCount}";
        }

        /// <summary>
        /// Show the claim button when the quest is completed
        /// </summary>
        /// <param name="quest"></param>
        private void ShowClaimButton(Quest quest)
        {
            _claimButton.SetActive(true);
        }

        #endregion
    }
}