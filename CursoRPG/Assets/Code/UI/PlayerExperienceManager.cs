using System;
using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerExperienceManager : MonoBehaviour
{
    [SerializeField]
    private Image _expBar;

    [SerializeField]
    private TextMeshProUGUI _experienceText;

    private float _fillSpeed = 1f;

    private void Start()
    {
        PlayerExperience.OnExpGained += UpdateExperienceBar;
        UpdateExperienceBar();
    }

    private void OnDisable()
    {
        PlayerExperience.OnExpGained -= UpdateExperienceBar;
    }

    private void UpdateExperienceBar()
    {
        var targetFill = PlayerExperience.Instance.CurrentTemporalExp / PlayerExperience.Instance.NextLevelExp;
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
        float expPercentage = PlayerExperience.Instance.CurrentTemporalExp / PlayerExperience.Instance.NextLevelExp;

        return $"{(expPercentage * 100):F0}%";
    }
}
