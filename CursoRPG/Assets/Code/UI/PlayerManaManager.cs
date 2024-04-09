using System;
using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerManaManager : MonoBehaviour
    {
        [SerializeField]
        private Image _manaBar;

        [SerializeField]
        private Image _manaUsedBarEffect;

        [SerializeField]
        private TextMeshProUGUI _manaText;

        private float _fillSpeed = 1f;
        private float _fillDelayedSpeed = 0.3f;
        private float _previewDelay = 0.7f;

        private bool _isManaUsed;

        private void Start()
        {
            PlayerMana.OnManaUsed += UpdateManaBar;

            UpdateManaBar();
            _isManaUsed = false;
        }

        private void Update() 
        {
            if(_isManaUsed)
            {
                return;
            }

            UpdateManaBarOnRegeneration();
        }

        private void OnDisable() 
        {
            PlayerMana.OnManaUsed -= UpdateManaBar;
        }

        private void UpdateManaBarOnRegeneration()
        {
            _manaBar.fillAmount = PlayerMana.Instance.CurrentMana / PlayerMana.Instance.MaxMana;
            _manaUsedBarEffect.fillAmount = PlayerMana.Instance.CurrentMana / PlayerMana.Instance.MaxMana;
            _manaText.text = PlayerMana.Instance.CurrentMana.ToString() + "/" + PlayerMana.Instance.MaxMana.ToString();
        }

        private void UpdateManaBar()
        {
            float targetFill = PlayerMana.Instance.CurrentMana / PlayerMana.Instance.MaxMana;

            StartCoroutine(UpdateDamagedFillOverTime(_manaBar, targetFill));

            StartCoroutine(DelayedUpdateDamagedFillOverTime(_manaUsedBarEffect, targetFill));

            _manaText.text = PlayerMana.Instance.CurrentMana.ToString() + "/" + PlayerMana.Instance.MaxMana.ToString();
        }
        
        private IEnumerator UpdateDamagedFillOverTime(Image image, float targetFill)
        {
            _isManaUsed = true;
            float currentFill = image.fillAmount;
            while (currentFill > targetFill)
            {
                currentFill -= _fillSpeed * Time.fixedDeltaTime;
                image.fillAmount = currentFill;
                yield return null;
            }

            image.fillAmount = targetFill;
        }

        private IEnumerator DelayedUpdateDamagedFillOverTime(Image image, float targetFill)
        {
            yield return new WaitForSeconds(_previewDelay);

            float currentFill = image.fillAmount;
            while (currentFill > targetFill)
            {
                currentFill -= _fillDelayedSpeed * Time.fixedDeltaTime;
                image.fillAmount = currentFill;
                yield return null;
            }

            image.fillAmount = targetFill;
            _isManaUsed = false;
        }
    }
}