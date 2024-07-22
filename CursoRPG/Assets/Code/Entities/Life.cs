using UnityEngine;

namespace Entities
{
    public class Life : MonoBehaviour, IDamageable
    {
        #region Private Attributes
        
        [SerializeField] private float _currentLife;

        [SerializeField] private float _maxLife;

        #endregion

        #region Properties
        
        public float MaxLife => _maxLife;
        public float CurrentLife => _currentLife;

        #endregion
        
        #region Methods

        /// <summary>
        /// Configure the life
        /// </summary>
        public virtual void Configure()
        {
            _currentLife = _maxLife;
        }

        /// <summary>
        /// Take damage from the entity with the damage amount. If the entity has no life left, call the Defeated method
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(float damage)
        {
            if(damage <= 0) { return; }

            _currentLife -= damage;

            if(_currentLife <= 0)
            {
                Defeated();
                return;
            }
        }

        /// <summary>
        /// Heal the entity with the heal amount. If the entity has more life than the max life, set the current life to the max life
        /// </summary>
        /// <param name="healAmount"></param>
        public virtual void Heal(float healAmount)
        {
            if(healAmount <= 0) { return; }

            _currentLife += healAmount;

            if(_currentLife > _maxLife)
            {
                _currentLife = _maxLife;
            }
        }

        /// <summary>
        /// Defeat the entity
        /// </summary>
        public virtual void Defeated() { }

        /// <summary>
        /// Check if the entity is defeated
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDefeated()
        {
            return _currentLife <= 0;
        }

        /// <summary>
        /// Set up the stats of the entity
        /// </summary>
        /// <param name="maxHealth"></param>
        public void SetUpStats(float maxHealth)
        {
            _maxLife = maxHealth;
        }

        #endregion
    }
}