using Quests;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIQuest : MonoBehaviour
    {
        #region Private Attributes
        
        [SerializeField] private TextMeshProUGUI _questNameText;
        [SerializeField] private TextMeshProUGUI _questDescriptionText;

        #endregion

        #region Properties

        public Quest QuestLoaded { get; set; }
        
        #endregion

        #region Methods

        public virtual void ConfigureQuest(Quest quest)
        {
            _questNameText.text = quest.QuestName;
            _questDescriptionText.text = quest.Description;

            QuestLoaded = quest;
        }
        
        #endregion
    }
}