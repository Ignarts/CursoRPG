using UnityEngine;

namespace Player
{
    public class PlayerAnimations
    {
        private Animator _animator;

        private const string HORIZONTAL_MOVE = "MoveX";
        private const string VERTICAL_MOVE = "MoveY";

        private const string IDLE_LAYER = "Idle";
        private const string MOVEMENT_LAYER = "Walk";
        private const string DEFEATED_ANIMATION = "Defeated";

        public PlayerAnimations(Animator animator)
        {
            _animator = animator;
            PlayerLife.OnPlayerDefeated += PlayDefeatAnimation;
            PlayerLife.OnPlayerRevived += PlayerRevivedAnimation;
        }

        private void OnDisable()
        {
            PlayerLife.OnPlayerDefeated -= PlayDefeatAnimation;
            PlayerLife.OnPlayerRevived -= PlayerRevivedAnimation;
        }

        public void PlayMovementAnimation(Vector2 moveInput)
        {
            _animator.SetFloat(HORIZONTAL_MOVE, moveInput.x);
            _animator.SetFloat(VERTICAL_MOVE, moveInput.y);
        }

        public void UpdateActualLayer(Vector2 moveInput)
        {
            if (moveInput == Vector2.zero)
            {
                ActivateLayer(IDLE_LAYER);
            }
            else
            {
                ActivateLayer(MOVEMENT_LAYER);
            }
        }

        private void ActivateLayer(string layerName)
        {
            for (int i = 0; i < _animator.layerCount; i++)
            {
                _animator.SetLayerWeight(i, 0);
            }

            _animator.SetLayerWeight(_animator.GetLayerIndex(layerName), 1);
        }

        private void PlayDefeatAnimation()
        {
            ActivateLayer(IDLE_LAYER);
            _animator.SetBool(DEFEATED_ANIMATION, true);
        }

        private void PlayerRevivedAnimation()
        {
            ActivateLayer(IDLE_LAYER);
            _animator.SetBool(DEFEATED_ANIMATION, false);
        }
    }
}
