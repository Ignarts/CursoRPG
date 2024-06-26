using System;
using UnityEngine;
using Entities;
using Managers;

namespace Player
{
    public class PlayerLife : Life
    {
        public static event Action OnLifeIncreased;
        public static event Action OnLifeDecreased;
        public static event Action OnPlayerDefeated;
        public static event Action OnPlayerRevived;
        private bool _isPlayerAlive;

        public bool IsPlayerAlive => _isPlayerAlive;

#region MonoBehaviour Methods

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
#endregion

#region Methods

        public override void Configure()
        {
            base.Configure();
            _isPlayerAlive = true;
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

#endregion
    }
}