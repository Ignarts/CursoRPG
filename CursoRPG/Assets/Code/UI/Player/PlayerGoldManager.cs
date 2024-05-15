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
            _goldText.text = gold.ToString();
        }
        
        #endregion
    }

}