using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class QuestsPanel : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private GameObject _questsPanel;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _questsPanel.SetActive(false);
        }

        private void Update()
        {
            if(!Keyboard.current.escapeKey.wasPressedThisFrame || !_questsPanel.activeSelf)
            {
                return;
            }

            HideQuestsPanel();
        }
        
        #endregion

        #region Methods

        public void ToggleQuestPanel()
        {
            if(_questsPanel.activeSelf)
            {
                HideQuestsPanel();
                return;
            }

            ShowQuestsPanel();
        }

        public void ShowQuestsPanel()
        {
            _questsPanel.SetActive(true);
        }

        public void HideQuestsPanel()
        {
            _questsPanel.SetActive(false);
        }

        #endregion
    }
}