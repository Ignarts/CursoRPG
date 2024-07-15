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

        public virtual void Configure()
        {
            _currentLife = _maxLife;
        }

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

        public virtual void Heal(float healAmount)
        {
            if(healAmount <= 0) { return; }

            _currentLife += healAmount;

            if(_currentLife > _maxLife)
            {
                _currentLife = _maxLife;
            }
        }

        public virtual void Defeated() { }

        public void SetUpStats(float maxHealth)
        {
            _maxLife = maxHealth;
        }

        #endregion
    }
}