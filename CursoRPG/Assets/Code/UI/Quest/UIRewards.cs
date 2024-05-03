using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum RewardType
    {
        Gold,
        Experience,
        Item
    }
    
    public class UIRewards : MonoBehaviour
    {
        #region Private Attributes
        
        [SerializeField] private RewardType _rewardType;
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private TextMeshProUGUI _rewardsAmountText;

        [Title("Rewards Icons")]
        [SerializeField] private Sprite _goldIcon;
        [SerializeField] private Sprite _experienceIcon;


        #endregion

        #region Methods
        
        /// <summary>
        /// Configure the reward based on the reward type
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="rewardType"></param>
        /// <param name="itemImage"></param>
        public void ConfigureReward(int amount, RewardType rewardType)
        {
            _rewardsAmountText.text = amount.ToString();
            SetRewardIcon(rewardType);
        }

        public void ConfigureReward(int amount, Sprite itemImage)
        {
            _rewardsAmountText.text = amount.ToString();
            SetRewardIcon(itemImage);
        }

        /// <summary>
        /// Set the reward icon based on the reward type
        /// </summary>
        /// <param name="rewardType"></param>
        private void SetRewardIcon(RewardType rewardType)
        {
            switch(rewardType)
            {
                case RewardType.Gold:
                    _rewardIcon.sprite = _goldIcon;
                    break;
                case RewardType.Experience:
                    _rewardIcon.sprite = _experienceIcon;
                    break;                   
            }
        }

        /// <summary>
        /// Set the reward icon based for the Item Reward
        /// </summary>
        /// <param name="rewardType"></param>
        /// <param name="itemImage"></param>
        private void SetRewardIcon(Sprite itemImage)
        {
            _rewardIcon.sprite = itemImage;
        }
        
        #endregion
    }
}