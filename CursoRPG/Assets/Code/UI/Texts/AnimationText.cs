using TMPro;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    #region Private Attributes

    [SerializeField] private TextMeshProUGUI _damageText;
    
    #endregion

    #region Methods

    /// <summary>
    /// Set the text of the animation text using a float.
    /// </summary>
    /// <param name="damage"></param>
    public void SetText(float damage)
    {
        _damageText.text = damage.ToString();
    }

    /// <summary>
    /// Set the text of the animation text using a string.
    /// </summary>
    /// <param name="text"></param>
    public void SetText(string text)
    {
        _damageText.text = text;
    }

    #endregion
}
