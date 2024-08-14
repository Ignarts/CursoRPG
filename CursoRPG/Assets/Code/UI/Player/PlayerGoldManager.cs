using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerGoldManager : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private TextMeshProUGUI _goldText;

        private int _gold;

        #endregion

        #region Properties

        public int Gold => _gold;
        
        #endregion

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            GoldManager.OnGoldChanged += UpdateGoldText;
        }

        private void OnDisable()
        {
            GoldManager.OnGoldChanged -= UpdateGoldText;
        }
        
        #endregion

        #region Methods

        public void UpdateGoldText(int gold)
        {
            _gold = gold;
            _goldText.text = _gold.ToString();
        }
        
        #endregion
    }

}