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
        
        #endregion
        
        #region UIQuest Base Methods

        public override void ConfigureQuest(Quest quest)
        {
            base.ConfigureQuest(quest);

            _questProgressText.text = $"{quest.CurrentObjectiveCount}/{quest.ObjectiveCount}";

            ConfigureRewards(quest);

            _claimButton.SetActive(quest.IsQuestCompleted);
        }

        private void ConfigureRewards(Quest quest)
        {
            UIRewards goldRewards = Instantiate(_uIRewards, _container);
            goldRewards.ConfigureReward(quest.GoldReward, RewardType.Gold);

            UIRewards xpRewards = Instantiate(_uIRewards, _container);
            xpRewards.ConfigureReward(quest.ExperienceReward, RewardType.Experience);

            foreach ( var QuestRewardItem in quest.QuestRewardItems)
            {
                UIRewards rewards = Instantiate(_uIRewards, _container);
                rewards.ConfigureReward(QuestRewardItem.Amount, QuestRewardItem.InventoryItemRewarded.Icon);
            }
        }

        #endregion
    }
}