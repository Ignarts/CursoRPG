using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Dialogs
{
    public class DialogInteraction : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private NPCDialog _npcDialog;
        [SerializeField] private GameObject _interactionPanel;
        private bool _isPlayerInInteractionArea;

        #endregion

        #region Properties

        public NPCDialog NpcDialog => _npcDialog;

        #endregion

        #region MonoBehaviour Methods

        /// <summary>
        /// Awake Method
        /// </summary>
        private void Awake()
        {
            _isPlayerInInteractionArea = false;
            _interactionPanel.SetActive(false);
        }

        private void Update()
        {
            if(!_isPlayerInInteractionArea)
            {
                return;
            }

            if(Keyboard.current.eKey.wasPressedThisFrame && !DialogManager.Instance.IsInDialog)
            {
                DialogManager.Instance.SetUpDialogInfo(_npcDialog);
            }
        }

        /// <summary>
        /// OnTriggerEnter2D Method
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                _isPlayerInInteractionArea = true;
                _interactionPanel.SetActive(true);
            }
        }

        /// <summary>
        /// OnTriggerExit2D Method
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                _isPlayerInInteractionArea = false;
                _interactionPanel.SetActive(false);
            }
        }

        #endregion
    }
}