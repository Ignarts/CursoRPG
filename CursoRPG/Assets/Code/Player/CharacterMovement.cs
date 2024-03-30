using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private float _speed = 8.0f;

        [SerializeField]
        private Animator _animator;

        private PlayerAnimations _playerAnimations;

        private Vector2 _moveInput;

        private Keyboard _keyboard;

        private void Awake()
        {
            _keyboard = Keyboard.current;
            _playerAnimations = new PlayerAnimations(_animator);
        }

        private void Update()
        {
            _moveInput = GetMovementInput();
            _playerAnimations.UpdateActualLayer(_moveInput);

            if(_moveInput == Vector2.zero) { return; }

            _playerAnimations.SetMovementAnimation(_moveInput);
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _moveInput.normalized * _speed * Time.fixedDeltaTime);
        }

        private Vector2 GetMovementInput()
        {
            float xInputValue = _keyboard.dKey.ReadValue() - _keyboard.aKey.ReadValue();
            float yInputValue = _keyboard.wKey.ReadValue() - _keyboard.sKey.ReadValue();

            return new Vector2(xInputValue, yInputValue);
        }
    }
}
