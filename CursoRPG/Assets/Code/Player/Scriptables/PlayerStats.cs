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
        [SerializeField] protected float _manaRegeneration;
        [SerializeField] protected float _defense;
        [SerializeField] protected float _criticalChance;
        [SerializeField] protected float _criticalBonus;
        [SerializeField] protected float _blockChance;
        [SerializeField] protected float _blockBonus;

        [Header("Player Attributes")]
        [SerializeField] protected float _strength;
        [SerializeField] protected float _intelligence;
        [SerializeField] protected float _dexterity;

        private int _availablePoints;

        #endregion

        #region Properties

        public float MaxMana => _maxMana;
        public float ManaRegeneration => _manaRegeneration;
        public float Defense => _defense;
        public float CriticalChance => _criticalChance;
        public float CriticalBonus => _criticalBonus;
        public float BlockChance => _blockChance;
        public float BlockBonus => _blockBonus;

        public float Strength => _strength;
        public float Intelligence => _intelligence;
        public float Dexterity => _dexterity;

        public int AvailablePoints => _availablePoints;

        #endregion

        #region Methods

        /// <summary>
        /// Method to reset all player stats and attributes to their default values.
        /// </summary>
        public void ResetStats()
        {
            _maxHealth = 100;
            _manaRegeneration = 1;
            _damage = 2;
            _defense = 0;
            _speed = 8;
            _attackSpeed = 1.2f;
            _criticalChance = 0;
            _criticalBonus = 1.2f;
            _blockChance = 0;
            _blockBonus = 1.2f;
            _strength = 0;
            _intelligence = 0;
            _dexterity = 0;
            _availablePoints = 5;
        }

        public void AddAttributePoint(AttributeType attributeType)
        {
            if(_availablePoints <= 0)
                return;

            switch(attributeType)
            {
                case AttributeType.Strength:
                    _strength++;
                    break;
                case AttributeType.Intelligence:
                    _intelligence++;
                    break;
                case AttributeType.Dexterity:
                    _dexterity++;
                    break;
            }

            _availablePoints--;
        }

        #endregion
    }
    
    /// <summary>
    /// Enum that represents the attribute type of the player.
    /// </summary>
    public enum AttributeType
    {
        Strength,
        Intelligence,
        Dexterity
    }
}