using System.Collections;
using Entities.AI;
using UnityEngine;

namespace Player
{
    public class PlayerVFX : MonoBehaviour
    {
        #region Private Attributes
        
        [SerializeField] private GameObject _playerHitVFX;
        [SerializeField] private Transform _canvasHitVFXPosition;
        [SerializeField] private ObjectPooler _objectPooler;
        [SerializeField] private float _hitVFXTime = 1.5f;

        #endregion

        #region MonoBehaviour Methods

        private void Start() 
        {
            _objectPooler.CreatePooler(_playerHitVFX);
        }

        private void OnEnable()
        {
            AIController.OnDamageDealt += PlayHitVFX;
        }

        private void OnDisable()
        {
            AIController.OnDamageDealt -= PlayHitVFX;
        }
        
        #endregion

        #region Methods

        private void PlayHitVFX(float amount)
        {
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