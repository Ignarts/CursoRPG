using System;
using UnityEngine;
using Entities;

namespace Player
{
    public class PlayerLife : Life
    {
        public static event Action OnLifeIncreased;
        public static event Action OnLifeDecreased;
        public static event Action OnPlayerDefeated;

        [ContextMenu("Heal")]
        private void Heal()
        {
            Heal(10);
        }

        [ContextMenu("Take Damage")]
        private void TakeDamage()
        {
            TakeDamage(10);
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

        protected override void Awake()
        {
            base.Awake();
            OnLifeDecreased?.Invoke();
            OnPlayerDefeated?.Invoke();
        }
    }
}