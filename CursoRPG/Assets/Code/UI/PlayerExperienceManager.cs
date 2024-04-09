using System;
using System.Collections;
using Player;
using TMPro;
using UnityEngine;
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
        StartCoroutine(UpdateDamagedFillOverTime(_expBar, PlayerExperience.Instance.CurrentTemporalExp / PlayerExperience.Instance.NextLevelExp));
        
        _experienceText.text = $"Lvl{PlayerExperience.Instance.Level.ToString()}: " +
            PlayerExperience.Instance.CurrentTemporalExp.ToString()
            + "/" +
            PlayerExperience.Instance.NextLevelExp.ToString();
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
}
