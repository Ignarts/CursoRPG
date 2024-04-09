using System;
using Entities;
using UnityEngine;

namespace Player
{
    public class PlayerMana : Mana
    {
        public static PlayerMana Instance;

        public static event Action OnManaUsed;

        protected override void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            base.Awake();
        }

        protected override void ManaRegeneration()
        {
            if(!PlayerLife.Instance.IsPlayerAlive) { return; }

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