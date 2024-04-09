using UnityEngine;

namespace Entities.Scriptables
{
    public class EntityStats : ScriptableObject
    {
        #region Private Attributes

        protected float _maxHealth;
        protected float _damage;
        protected float _speed;
        protected float _attackSpeed;

        #endregion

        #region Properties

        public float MaxHealth => _maxHealth;
        public float Damage => _damage;
        public float Speed => _speed;
        public float AttackSpeed => _attackSpeed;
        
        #endregion
    }
}