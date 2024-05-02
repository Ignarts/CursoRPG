using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Entities.Dialogs
{
    public enum DialogExtraTypes
    {
        Quest,
        Shop,
        Crafting
    }

    [CreateAssetMenu(fileName = "NPCDialog", menuName = "Dialogs/NPCDialog")]
    public class NPCDialog : ScriptableObject
    {
        #region Private Attributes

        [Title("NPC Information")]
        [SerializeField] private string _npcName;
        [SerializeField] private Sprite _npcSprite;
        [SerializeField] private bool _hasExtraType;
        [ShowIf("_hasExtraType")]
        [SerializeField] private DialogExtraTypes _dialogExtraType;
        [SerializeField] private Dialog _dialog;

        #endregion

        #region Properties

        public string NpcName => _npcName;
        public Sprite NpcSprite => _npcSprite;
        public Dialog Dialog => _dialog;
        public bool HasExtraType => _hasExtraType;
        public DialogExtraTypes DialogExtraType => _dialogExtraType;

        #endregion
    }

    [Serializable]
    public struct Dialog
    {
        public List<string> DialogLines;
    }
}