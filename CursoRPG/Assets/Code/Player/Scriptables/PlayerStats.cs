using Entities.Scriptables;
using UnityEngine;

namespace Player.Scriptables
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "ScriptableObjects/PlayerStats", order = 0)]
    public class PlayerStats : EntityStats
    {
        #region Private Attributes

        protected float _maxMana;
        protected float _manaRegen;
        protected float defense;
        protected float _criticalChance;
        protected float _criticalBonus;
        protected float _blockChance;
        protected float _blockBonus;

        #endregion

        #region Properties

        public float MaxMana => _maxMana;
        public float ManaRegen => _manaRegen;
        public float Defense => defense;
        public float CriticalChance => _criticalChance;
        public float CriticalBonus => _criticalBonus;
        public float BlockChance => _blockChance;
        public float BlockBonus => _blockBonus;

        #endregion
    }
}