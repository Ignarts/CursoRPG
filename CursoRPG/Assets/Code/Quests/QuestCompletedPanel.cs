using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Quests
{
    public class QuestCompletedPanel : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private GameObject _questCompletedPanel;

        [Header("Visual Attributes")]
        [SerializeField] private TextMeshProUGUI _questName;
        [SerializeField] private UIRewards _questReward;
        [SerializeField] private Transform _rewardContainer;

        private Quest _questToClaim;
        
        #endregion
        
        #region Public Attributes
        #endregion
        
        #region MonoBehaviour Methods

        private void Awake()
        {
            _questCompletedPanel.SetActive(false);
        }

        private void Update()
        {
            if(!Keyboard.current.escapeKey.wasPressedThisFrame || !_questCompletedPanel.activeSelf)
            {
                return;
            }

            HideQuestsCompletedPanel();
        }


        #endregion

        #region Methods

        /// <summary>
        /// Configure the quest completed panel with the given quest data
        /// </summary>
        /// <param name="quest"></param>
        public void ConfigureQuestCompletedPanel(Quest quest)
        {            
            _questToClaim = quest;

            _questName.text = quest.QuestName;

            UIRewards goldRewards = Instantiate(_questReward, _rewardContainer);
            goldRewards.ConfigureReward(quest.GoldReward, RewardType.Gold);

            UIRewards xpRewards = Instantiate(_questReward, _rewardContainer);
            xpRewards.ConfigureReward(quest.ExperienceReward, RewardType.Experience);
            
            foreach (var reward in quest.QuestRewardItems)
            {
                UIRewards newReward = Instantiate(_questReward, _rewardContainer);
                newReward.ConfigureReward(reward.Amount, reward.InventoryItemRewarded.Icon);
            }

            ShowQuestsCompletedPanel();
        }

        public void ClaimQuest()
        {
            if(_questToClaim == null)
                return;
            
            // claim quest rewards
            GoldManager.Instance.AddGold(_questToClaim.GoldReward);
            PlayerExperience playerExp = CharacterMovement.Instance.GetComponent<PlayerExperience>();
            playerExp.AddExperience(_questToClaim.ExperienceReward);
            
            foreach(var item in _questToClaim.QuestRewardItems)
            {
                Inventory.Instance.AddItem(item.InventoryItemRewarded, item.Amount);
            }

            // remove the quest from the player quest list and hide the completed quest panel
            HideQuestsCompletedPanel();

            // remove the quest from quest to claim
            _questToClaim = null;
        }

        /// <summary>
        /// Show the quest completed panel
        /// </summary>
        public void ShowQuestsCompletedPanel()
        {
            _questCompletedPanel.SetActive(true);
        }

        /// <summary>
        /// Hide the quest completed panel
        /// </summary>
        public void HideQuestsCompletedPanel()
        {
            _questCompletedPanel.SetActive(false);
        }

        #endregion
    }
}