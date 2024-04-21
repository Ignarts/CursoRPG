using System;
using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerExperienceManager : MonoBehaviour
    {
#region Private Attributes

        [SerializeField]
        private Image _expBar;

        [SerializeField]
        private TextMeshProUGUI _experienceText;

        private PlayerExperience _playerExperience;
        private float _fillSpeed = 1f;

#endregion

#region MonoBehaviour Methods

        private void Start()
        {
            PlayerExperience.OnExpGained += UpdateExperienceBar;
            UpdateExperienceBar();
        }

        private void OnDisable()
        {
            PlayerExperience.OnExpGained -= UpdateExperienceBar;
        }

#endregion

#region Methods

        public void Configure(PlayerExperience playerExperience)
        {
            _playerExperience = playerExperience;
        }

        private void UpdateExperienceBar()
        {
            var targetFill = _playerExperience.CurrentTemporalExp / _playerExperience.NextLevelExp;
            StartCoroutine(UpdateDamagedFillOverTime(_expBar, targetFill));

            _experienceText.text = GetExperienceText();
        }

        private IEnumerator UpdateDamagedFillOverTime(Image image, float targetFill)
        {
            float currentFill = image.fillAmount;
            while (Math.Abs(currentFill - targetFill) > float.Epsilon)
            {
                currentFill = Mathf.MoveTowards(currentFill, targetFill, _fillSpeed * Time.fixedDeltaTime);
                image.fillAmount = currentFill;
                yield return null;
            }

            image.fillAmount = targetFill;
        }

        private string GetExperienceText()
        {
            float levelText = _playerExperience.Level;
            float expPercentage = _playerExperience.CurrentTemporalExp / _playerExperience.NextLevelExp;

            return $"Lvl {levelText}: {(expPercentage * 100):F0}%";
        }
        
#endregion
    }
}