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
            if(!_playerLife.IsPlayerAlive) { return; }

            base.UseMana(manaAmount);
            OnManaUsed?.Invoke();
        }

        public void RestoreMana(int manaAmount)
        {
            if(!_playerLife.IsPlayerAlive) { return; }

            _currentMana += manaAmount;
            if(_currentMana > _maxMana)
            {
                _currentMana = _maxMana;
            }
        }

        public void RegenerateAllMana()
        {
            _currentMana = _maxMana;
        }
    }
}