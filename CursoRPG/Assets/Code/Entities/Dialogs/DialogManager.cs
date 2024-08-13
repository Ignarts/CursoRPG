using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Entities.Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager Instance;

        #region Private Attributes

        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _dialogText;

        [Title("Extra Types Interaction References")]
        [SerializeField] private QuestsPanel _questsPanel;
        [SerializeField] private ShopPanel _shopPanel;

        private NPCDialog _currentDialog;
        private Queue<string> _dialogLines;
        private bool _isInDialog;
        private bool _isTyping;
        private float _dialogSpeed;
        
        #endregion

        #region Properties

        public bool IsInDialog => _isInDialog;
        
        #endregion

        #region Constants

        private const float DIALOG_TYPING_SPEED = 0.03f;
        
        #endregion

        #region MonoBehaviour Methods

        /// <summary>
        /// Awake Method
        /// </summary>
        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _dialogLines = new Queue<string>();
            ToggleDialogPanel(false);
        }

        private void Update()
        {
            if(!_dialogPanel.activeSelf)
                return;
            
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if(_isTyping)
                {
                    _dialogSpeed = 0;
                    return;
                }

                _dialogSpeed = DIALOG_TYPING_SPEED;
                DisplayNextDialog();
            }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Set up dialog info
        /// </summary>
        /// <param name="dialogInfo"></param>
        public void SetUpDialogInfo(NPCDialog dialogInfo)
        {
            ToggleDialogPanel(true);

            // set dialog info
            _currentDialog = dialogInfo;
            _icon.sprite = dialogInfo.NpcSprite;
            _name.text = dialogInfo.NpcName;
            _dialogSpeed = DIALOG_TYPING_SPEED;

            LoadDialogSequence(dialogInfo);

            DisplayNextDialog();

            Debug.Log($"<color=green> Set Up Dialog Info</color>\n" +
                        $"NPC name: <color=red>{dialogInfo.NpcName}</color>.\n");
        }

        /// <summary>
        /// Update active state of dialog panel
        /// </summary>
        /// <param name="value"></param>
        private void ToggleDialogPanel(bool value)
        {
            _dialogPanel.SetActive(value);
            _isInDialog = value;
        }

        /// <summary>
        /// Load dialog sequence
        /// </summary>
        /// <param name="dialog"></param>
        private void LoadDialogSequence(NPCDialog dialog)
        {
            _dialogLines.Clear();

            if(dialog.Dialog.DialogLines.Count == 0
                || dialog.Dialog.DialogLines == null)
            {
                Debug.LogWarning("Dialog is empty");
                return;
            }

            for(int line = 0; line < dialog.Dialog.DialogLines.Count; line++)
            {
                _dialogLines.Enqueue(dialog.Dialog.DialogLines[line]);
            }
        }

        /// <summary>
        /// Display next dialog
        /// </summary>
        private void DisplayNextDialog()
        {
            if(_dialogLines.Count == 0)
            {
                if(_currentDialog.HasExtraType)
                {
                    OpenExtraTypeInteractionPanel(_currentDialog.DialogExtraType);
                    
                }

                ToggleDialogPanel(false);
                _isInDialog = false;
                return;
            }

            string dialog = _dialogLines.Dequeue();
            StartCoroutine(AnimateDialog(dialog));
        }

        /// <summary>
        /// Coroutine to animate dialog character for character
        /// </summary>
        /// <param name="dialog"></param>
        /// <returns></returns>
        private IEnumerator AnimateDialog(string dialog)
        {
            _isTyping = true;

            _dialogText.text = string.Empty;
            char[] character = dialog.ToCharArray();

            for(int i = 0; i < character.Length; i++)
            {
                _dialogText.text += character[i];
                yield return new WaitForSeconds(_dialogSpeed);
            }

            _isTyping = false;
        }

        private void OpenExtraTypeInteractionPanel(DialogExtraTypes dialogExtraType)
        {
            switch(dialogExtraType)
            {
                case DialogExtraTypes.Quest:
                    _questsPanel.ShowQuestsPanel();
                    break;
                case DialogExtraTypes.Shop:
                    _shopPanel.ShowShopPanel();
                    break;

            }
        }
        
        #endregion
    }
}