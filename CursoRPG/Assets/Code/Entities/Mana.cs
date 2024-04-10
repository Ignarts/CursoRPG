using System.Collections;
using UnityEngine;

namespace Entities
{
    public class Mana : MonoBehaviour
    {
        [SerializeField]
        protected int _currentMana;

        [SerializeField]
        protected int _maxMana;

        [SerializeField]
        protected float _manaRegenerationRate;

        [SerializeField]
        protected int _manaRegenerationValue;

        private float _currentManaregenerationTime;

        public float MaxMana => _maxMana;
        public float CurrentMana => _currentMana;

#region MonoBehaviour Methods

        private IEnumerator Start()
        {
            while(true)
            {
                ManaRegeneration();
                yield return null;
            }
        }

#endregion

#region Methods

        public virtual void Configure()
        {
            _currentMana = _maxMana;
            _currentManaregenerationTime = 0f;
        }

        protected virtual void ManaRegeneration()
        {
            if(_currentMana >= _maxMana) { return; }

            _currentManaregenerationTime += Time.deltaTime;

            if(_currentManaregenerationTime < _manaRegenerationValue) { return; }

            _currentMana += _manaRegenerationValue;
            _currentManaregenerationTime = 0f;

            if(_currentMana > _maxMana)
            {
                _currentMana = _maxMana;
            }
        }

        protected virtual void UseMana(int manaAmount)
        {
            if(manaAmount <= 0 || _currentMana < manaAmount) { return; }

            _currentMana -= manaAmount;
        }

        public void SetUpStats(int maxMana, float manaRegenerationRate)
        {
            _maxMana = maxMana;
            _manaRegenerationRate = manaRegenerationRate;
        }
        
#endregion
    }
}