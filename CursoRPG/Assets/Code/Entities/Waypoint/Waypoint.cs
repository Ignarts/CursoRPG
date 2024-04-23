using UnityEngine;

namespace Entities
{
    public class Waypoint : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3[] _points;

        private Vector3 _startPosition;

        private const float RADIUS = 0.5f;
        
        #endregion

        #region Properties

        public Vector3[] Points => _points;
        public Vector3 StartPosition => _startPosition;
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _startPosition = _transform.position;
        }
        
        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            _startPosition = _transform.position;
#endif

            if(_points == null || _points.Length == 0) return;

            Gizmos.color = Color.blue;
            for (int point = 0; point < _points.Length; point++)
            {
                Gizmos.DrawWireSphere(_points[point] + _startPosition, RADIUS);

                if(point < _points.Length - 1)
                {
                    Gizmos.DrawLine(_points[point] + _startPosition, _points[point + 1] + _startPosition);
                }
            }
        }
        
        #endregion
    }
}