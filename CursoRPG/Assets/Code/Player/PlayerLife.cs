using System;
using UnityEngine;
using Entities;

namespace Player
{
    public class PlayerLife : Life
    {
        public static PlayerLife Instance;

        public static event Action OnLifeIncreased;
        public static event Action OnLifeDecreased;
        public static event Action OnPlayerDefeated;
        public static event Action OnPlayerRevived;
        private bool _isPlayerAlive;

        public bool IsPlayerAlive => _isPlayerAlive;

        protected override void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            
            Instance = this;

            base.Awake();
            _isPlayerAlive = true;
        }

        private void Update()
        {
            if(CurrentLife <= 0 && _isPlayerAlive)
            {
                OnPlayerDefeated?.Invoke();
                _isPlayerAlive = false;
            }

            if(CurrentLife > 0 && !_isPlayerAlive)
            {
                OnPlayerRevived?.Invoke();
                _isPlayerAlive = true;
            }
        }

        public override void Heal(float healAmount)
        {
            base.Heal(healAmount);
            OnLifeIncreased?.Invoke();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            OnLifeDecreased?.Invoke();
        }
    }
}