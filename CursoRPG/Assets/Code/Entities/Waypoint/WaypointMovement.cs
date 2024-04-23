using System.Collections;
using UnityEngine;

namespace Entities
{
    public enum MovementState
    {
        Moving,
        Waiting
    }

    public class WaypointMovement : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Waypoint _waypoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _speed;
        [SerializeField] private bool _waitOnPoint;
        [SerializeField] private float _waitTime;

        private MovementState _movementState;
        private int _actualPointIndex;
        private bool _isOnPoint;

        #endregion

        #region Properties

        public Vector3 NextWaypointPoint => _waypoint.GetNextPoint(_actualPointIndex);
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _actualPointIndex = 0;
            _movementState = MovementState.Moving;
            _isOnPoint = false;
        }

        protected virtual void Update()
        {
            if(!_waitOnPoint)
            {
                MoveEntity();
                return;
            }
            
            switch (_movementState)
            {
                case MovementState.Moving:
                    MoveEntity();
                    break;
                case MovementState.Waiting:
                    WaitInIdle();
                    break;
            }
        }

        #endregion

        #region Methods

        protected virtual void MoveEntity()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, NextWaypointPoint, _speed * Time.deltaTime);

            if(_transform.position == NextWaypointPoint)
            {
                _actualPointIndex = (_actualPointIndex + 1) % _waypoint.Points.Length;
                _movementState = MovementState.Waiting;
                _isOnPoint = true;
                return;
            }
            
            FlipSprite();
        }

        protected virtual void WaitInIdle()
        {
            if(_isOnPoint)
            {
                StartCoroutine(WaitInIdleCoroutine());
                _isOnPoint = false;
            }
        }

        protected virtual IEnumerator WaitInIdleCoroutine()
        {
            yield return new WaitForSeconds(_waitTime);

            _movementState = MovementState.Moving;
        }

        private void FlipSprite()
        {
            if(_transform.position.x < NextWaypointPoint.x)
            {
                _spriteRenderer.flipX = false;
                return;
            }

            _spriteRenderer.flipX = true;
        }
        
        #endregion
    }
}