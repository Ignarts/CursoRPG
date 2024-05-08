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

        public override void ConfigureQuest(Quest quest)
        {
            base.ConfigureQuest(quest);

            _quest = quest;
            _questProgressText.text = $"{_quest.CurrentObjectiveCount}/{_quest.ObjectiveCount}";

            ConfigureRewards();

            _claimButton.SetActive(_quest.IsQuestCompleted);
        }

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

        private void AddProgress(Quest quest)
        {
            if(quest != _quest)
                return;

            _questProgressText.text = $"{quest.CurrentObjectiveCount}/{quest.ObjectiveCount}";
        }

        private void ShowClaimButton(Quest quest)
        {
            _claimButton.SetActive(true);
        }

        #endregion
    }
}