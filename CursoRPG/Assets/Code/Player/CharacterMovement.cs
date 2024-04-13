using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CharacterMovement : MonoBehaviour
    {
        public static CharacterMovement Instance;

        #region Private Attributes

        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private float _speed = 8.0f;

        [SerializeField] private Animator _animator;

        [SerializeField] private PlayerLife _playerLife;

        private PlayerAnimations _playerAnimations;
        private Vector2 _moveInput;
        private Keyboard _keyboard;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _keyboard = Keyboard.current;
            _playerAnimations = new PlayerAnimations(_animator);
        }

        private void Update()
        {
            if (!_playerLife.IsPlayerAlive) { return; }

            _moveInput = GetMovementInput();
            _playerAnimations.UpdateActualLayer(_moveInput);

            if (_moveInput == Vector2.zero) { return; }

            _playerAnimations.PlayMovementAnimation(_moveInput);
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _moveInput.normalized * _speed * Time.fixedDeltaTime);
        }
        #endregion

        #region Methods

        private Vector2 GetMovementInput()
        {
            float xInputValue = _keyboard.dKey.ReadValue() - _keyboard.aKey.ReadValue();
            float yInputValue = _keyboard.wKey.ReadValue() - _keyboard.sKey.ReadValue();

            return new Vector2(xInputValue, yInputValue);
        }

        public void SetUpStats(float speed)
        {
            _speed = speed;
        }

        #endregion
    }
}
