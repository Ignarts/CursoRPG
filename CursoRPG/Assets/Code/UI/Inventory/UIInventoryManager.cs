using System.Collections;
using UnityEngine;

namespace UI
{
    public class UIInventoryManager : MonoBehaviour
    {
        #region Private Attributes

        [Header("Menu Positions")]
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _restPosition;
        [SerializeField] private Transform _activePosition;
        [SerializeField] private float _speed;

        [Space(10)]
        [SerializeField] private GameObject _background;

        private Transform _panelPosition;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _panelPosition = _restPosition;
            _background.SetActive(false);
            StartCoroutine(MovePanel());
        }

        #endregion

        #region Methods

        public void ToggleInventoryPanel()
        {
            _panelPosition = _panelPosition == _restPosition ? _activePosition : _restPosition;
            _background.SetActive(_panelPosition == _activePosition);
            StartCoroutine(MovePanel());
        }

        private IEnumerator MovePanel()
        {
            while (Vector3.Distance(_transform.position, _panelPosition.position) > 0.1f)
            {
                _transform.position = Vector3.Lerp(_transform.position, _panelPosition.position, _speed * Time.deltaTime);
                yield return null;
            }
        }
        
        #endregion
    }
}