using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyLifeBar : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Image _lifeBar;
        [SerializeField] private float _speed = 5.0f;

        private float _actualLife;
        private float _maxLife;

        #endregion

        #region MonoBehaviour Methods

        /// <summary>
        /// Update method from MonoBehaviour
        /// </summary>
        private void Update()
        {
            if(_actualLife == _lifeBar.fillAmount)
                return;

            _lifeBar.fillAmount = Mathf.Lerp(_lifeBar.fillAmount, _actualLife / _maxLife, Time.deltaTime * _speed);
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Change the current life of the enemy
        /// </summary>
        /// <param name="life"></param>
        /// <param name="maxLife"></param>
        public void ChangeCurrentLife(float life, float maxLife)
        {
            _actualLife = life;
            _maxLife = maxLife;
        }

        #endregion
    }
}
