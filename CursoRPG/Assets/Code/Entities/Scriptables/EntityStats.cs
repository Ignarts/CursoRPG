using UnityEngine;

namespace Entities.Scriptables
{
    public class EntityStats : ScriptableObject
    {
        #region Private Attributes

        [Header("Entity Stats")]
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _attackSpeed;

        #endregion

        #region Properties

        public float MaxHealth => _maxHealth;
        public float Damage => _damage;
        public float Speed => _speed;
        public float AttackSpeed => _attackSpeed;
        
        #endregion
    }
}