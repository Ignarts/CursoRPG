using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField]
    private Image _lifeBar;

    [SerializeField]
    private Image _hitBarEffect;

    [SerializeField]
    private PlayerLife _playerLife;

    private float _fillSpeed = 1f;
    private float _fillDelayedSpeed = 0.3f;
    private float _hitDelay = 0.7f;


    private void Start()
    {
        PlayerLife.OnLifeIncreased += UpdateHealLifeBar;
        PlayerLife.OnLifeDecreased += UpdateHitLifeBar;

        _lifeBar.fillAmount = 1;
        _hitBarEffect.fillAmount = 1;
    }

    private void OnDisable()
    {
        PlayerLife.OnLifeIncreased -= UpdateHealLifeBar;
        PlayerLife.OnLifeDecreased -= UpdateHitLifeBar;
    }

#region Damage Methods
    private void UpdateHitLifeBar()
    {
        float targetFill = _playerLife.CurrentLife / _playerLife.MaxLife;

        StartCoroutine(UpdateDamagedFillOverTime(_lifeBar, targetFill));

        StartCoroutine(DelayedUpdateDamagedFillOverTime(_hitBarEffect, targetFill));
    }

    private IEnumerator UpdateDamagedFillOverTime(Image image, float targetFill)
    {
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
        yield return new WaitForSeconds(_hitDelay);

        float currentFill = image.fillAmount;
        while (currentFill > targetFill)
        {
            currentFill -= _fillDelayedSpeed * Time.fixedDeltaTime;
            image.fillAmount = currentFill;
            yield return null;
        }

        image.fillAmount = targetFill;
    }
#endregion

#region Heal Methods
    private void UpdateHealLifeBar()
    {
        float targetFill = _playerLife.CurrentLife / _playerLife.MaxLife;

        StartCoroutine(UpdateHealFillOverTime(_hitBarEffect, targetFill));

        StartCoroutine(DelayedUpdateHealFillOverTime(_lifeBar, targetFill));
    }

    private IEnumerator UpdateHealFillOverTime(Image image, float targetFill)
    {
        float currentFill = image.fillAmount;
        while (currentFill < targetFill)
        {
            currentFill += _fillSpeed * Time.fixedDeltaTime;
            image.fillAmount = currentFill;
            yield return null;
        }

        image.fillAmount = targetFill;
    }

    private IEnumerator DelayedUpdateHealFillOverTime(Image image, float targetFill)
    {
        yield return new WaitForSeconds(_hitDelay);

        float currentFill = image.fillAmount;
        while (currentFill < targetFill)
        {
            currentFill += _fillDelayedSpeed * Time.fixedDeltaTime;
            image.fillAmount = currentFill;
            yield return null;
        }

        image.fillAmount = targetFill;
    }
    #endregion
}
