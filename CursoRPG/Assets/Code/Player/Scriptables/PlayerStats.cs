using System;
using Entities.Scriptables;
using UnityEngine;

namespace Player.Scriptables
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "ScriptableObjects/PlayerStats", order = 0)]
    public class PlayerStats : EntityStats
    {
        #region Private Attributes

        [Header("Player Stats")]
        [SerializeField] protected float _maxMana;
        [SerializeField] protected float _manaRegen;
        [SerializeField] protected float _defense;
        [SerializeField] protected float _criticalChance;
        [SerializeField] protected float _criticalBonus;
        [SerializeField] protected float _blockChance;
        [SerializeField] protected float _blockBonus;

        #endregion

        #region Properties

        public float MaxMana => _maxMana;
        public float ManaRegen => _manaRegen;
        public float Defense => _defense;
        public float CriticalChance => _criticalChance;
        public float CriticalBonus => _criticalBonus;
        public float BlockChance => _blockChance;
        public float BlockBonus => _blockBonus;

        #endregion

        #region Methods

        public void ResetStats()
        {
            _maxHealth = 100;
            _manaRegen = 1;
            _damage = 2;
            _defense = 0;
            _speed = 8;
            _attackSpeed = 1.2f;
            _criticalChance = 0;
            _criticalBonus = 1.2f;
            _blockChance = 0;
            _blockBonus = 1.2f;
        }

        #endregion
    }
}