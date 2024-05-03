using Quests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UINPCQuest : UIQuest
    {
        #region Private Attributes

        [SerializeField] private TextMeshProUGUI _questRewardsText;
        [SerializeField] private Button _acceptButton;

        #endregion
        
        #region UIQuest Base Methods
        
        public override void ConfigureQuest(Quest quest)
        {
            base.ConfigureQuest(quest);
            string rewardsText = $"Gold:{quest.GoldReward}\nXP: {quest.ExperienceReward}\nItems:";

            for(int items =  0; items< quest.QuestRewardItems.Length; items++)
            {
                rewardsText += $" {quest.QuestRewardItems[items].Amount}x {quest.QuestRewardItems[items].InventoryItemRewarded.ItemName}\n";
            }

            _questRewardsText.text = rewardsText;

            _acceptButton.onClick.RemoveListener(AcceptQuest);
            _acceptButton.onClick.AddListener(AcceptQuest);
        }

        public void AcceptQuest()
        {
            if(QuestLoaded == null)
                return;

            PlayerQuestManager.Instance.AddQuestToActive(QuestLoaded);
            Destroy(gameObject);
        }

        #endregion
    }
}