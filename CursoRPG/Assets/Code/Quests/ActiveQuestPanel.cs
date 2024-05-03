using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveQuestPanel : MonoBehaviour
{
    #region Private Attributes

        [SerializeField] private GameObject _activeQuestsPanel;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _activeQuestsPanel.SetActive(false);
        }

        private void Update()
        {
            if(!Keyboard.current.escapeKey.wasPressedThisFrame || !_activeQuestsPanel.activeSelf)
            {
                return;
            }

            HideQuestsPanel();
        }
        
        #endregion

        #region Methods

        public void ToggleQuestPanel()
        {
            if(_activeQuestsPanel.activeSelf)
            {
                HideQuestsPanel();
                return;
            }

            ShowQuestsPanel();
        }

        public void ShowQuestsPanel()
        {
            _activeQuestsPanel.SetActive(true);
        }

        public void HideQuestsPanel()
        {
            _activeQuestsPanel.SetActive(false);
        }

        #endregion
}
