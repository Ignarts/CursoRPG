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
        [SerializeField] protected float defense;
        [SerializeField] protected float _criticalChance;
        [SerializeField] protected float _criticalBonus;
        [SerializeField] protected float _blockChance;
        [SerializeField] protected float _blockBonus;

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