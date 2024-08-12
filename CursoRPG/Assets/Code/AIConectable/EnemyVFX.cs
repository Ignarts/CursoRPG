using System.Collections;
using Player;
using UnityEngine;

namespace Entities.AI
{
    public class EnemyVFX : MonoBehaviour
    {
        #region Private Attributes
        
        [SerializeField] private GameObject _enemyHitVFX;
        [SerializeField] private Transform _canvasHitVFXPosition;
        [SerializeField] private ObjectPooler _objectPooler;
        [SerializeField] private float _hitVFXTime = 1.5f;

        private EnemyLife _enemyLife;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _enemyLife = GetComponent<EnemyLife>();
        }

        private void Start() 
        {
            _objectPooler.CreatePooler(_enemyHitVFX);
        }

        private void OnEnable()
        {
            PlayerAttack.OnDealtDamage += PlayEnemyHitVFX;
        }

        private void OnDisable()
        {
            PlayerAttack.OnDealtDamage -= PlayEnemyHitVFX;
        }
        
        #endregion

        #region Methods

        private void PlayEnemyHitVFX(float amount, EnemyLife enemyLife)
        {
            if(enemyLife != _enemyLife)
                return;
            
            StartCoroutine(PlayHitVFXCoroutine(amount));
        }

        private IEnumerator PlayHitVFXCoroutine(float amount)
        {
            GameObject newHitVFX = _objectPooler.GetInstance();
            AnimationText animationText = newHitVFX.GetComponent<AnimationText>();
            animationText.SetText(amount);

            newHitVFX.transform.SetParent(_canvasHitVFXPosition);
            newHitVFX.transform.position = _canvasHitVFXPosition.position;
            newHitVFX.SetActive(true);

            yield return new WaitForSeconds(_hitVFXTime);

            newHitVFX.SetActive(false);
            newHitVFX.transform.SetParent(_objectPooler.PoolContainer.transform);
        }
        
        #endregion
    }
}
