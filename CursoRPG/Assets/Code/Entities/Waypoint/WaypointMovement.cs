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

        [SerializeField] protected Waypoint _waypoint;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _speed;
        [SerializeField] protected bool _waitOnPoint;
        [SerializeField] protected float _waitTime;

        protected MovementState _movementState;
        protected int _actualPointIndex;
        protected bool _isOnPoint;

        private const string ANIMATION_WALK_VALUE = "Walk";
        private const string ANIMATION_IDLE_VALUE = "Idle";

        #endregion

        #region Properties

        public Vector3 NextWaypointPoint => _waypoint.GetNextPoint(_actualPointIndex);
        public float Speed => _speed;
        
        #endregion

        #region MonoBehaviour Methods

        protected virtual void Awake()
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
            
            PlayAnimation();
        }

        protected virtual void WaitInIdle()
        {
            if(_isOnPoint)
            {
                _animator.SetFloat(ANIMATION_WALK_VALUE, 0);
                StartCoroutine(WaitInIdleCoroutine());
                _isOnPoint = false;
            }
        }

        protected virtual IEnumerator WaitInIdleCoroutine()
        {
            yield return new WaitForSeconds(_waitTime);

            _movementState = MovementState.Moving;
        }


        private void PlayAnimation()
        {
            if(_transform.position.x < NextWaypointPoint.x)
            {
                _animator.SetFloat(ANIMATION_WALK_VALUE, 1);
                _animator.SetFloat(ANIMATION_IDLE_VALUE, 1);
                FlipSprite(false);
                return;
            }

            if(_transform.position.x > NextWaypointPoint.x)
            {
                _animator.SetFloat(ANIMATION_WALK_VALUE, 1);
                _animator.SetFloat(ANIMATION_IDLE_VALUE, 1);
                FlipSprite(true);
                return;
            }

            if(_transform.position.y < NextWaypointPoint.y)
            {
                _animator.SetFloat(ANIMATION_WALK_VALUE, 2);
                _animator.SetFloat(ANIMATION_IDLE_VALUE, 2);
                return;
            }

            if(_transform.position.y > NextWaypointPoint.y)
            {
                _animator.SetFloat(ANIMATION_WALK_VALUE, 3);
                _animator.SetFloat(ANIMATION_IDLE_VALUE, 3);
                return;
            }
        }

        private void FlipSprite(bool flip)
        {
            _spriteRenderer.flipX = flip;
        }
        
        #endregion
    }
}