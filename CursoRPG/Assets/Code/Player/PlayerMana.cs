using System;
using Entities;
using UnityEngine;

namespace Player
{
    public class PlayerMana : Mana
    {
        private PlayerLife _playerLife;

        public static event Action OnManaUsed;

        public void Configure(PlayerLife playerLife)
        {
            base.Configure();

            _playerLife = playerLife;
        }

        protected override void ManaRegeneration()
        {
            if(!_playerLife.IsPlayerAlive) { return; }

            base.ManaRegeneration();
        }

        protected override void UseMana(int manaAmount)
        {
            if(!PlayerLife.Instance.IsPlayerAlive) { return; }

            base.UseMana(manaAmount);
            OnManaUsed?.Invoke();
        }

        public void RegenerateAllMana()
        {
            _currentMana = _maxMana;
        }
    }
}