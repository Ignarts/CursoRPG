using UnityEngine;

namespace Entities
{
    public class Waypoint : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3[] _points;

        private Vector3 _actualPosition;

        private bool _isInitialized;
        private const float RADIUS = 0.5f;
        
        #endregion

        #region Properties

        public Vector3[] Points => _points;
        public Vector3 ActualPosition => _actualPosition;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _actualPosition = _transform.position;
            _isInitialized = true;
        }
        
        #endregion

        #region Methods

        public Vector3 GetNextPoint(int index)
        {
            return _actualPosition + _points[index];
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            if (!_isInitialized)
                _actualPosition = _transform.position;

            if(_points == null || _points.Length == 0) return;

            Gizmos.color = Color.blue;
            for (int point = 0; point < _points.Length; point++)
            {
                Gizmos.DrawWireSphere(_points[point] + _actualPosition, RADIUS);

                if(point < _points.Length - 1)
                {
                    Gizmos.DrawLine(_points[point] + _actualPosition, _points[point + 1] + _actualPosition);
                }
            }
        }
        
        #endregion
    }
}