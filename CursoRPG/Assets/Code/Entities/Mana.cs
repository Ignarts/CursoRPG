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

        protected virtual void Awake()
        {
            _currentMana = _maxMana;
            _currentManaregenerationTime = 0f;
        }

        private IEnumerator Start()
        {
            while(true)
            {
                ManaRegeneration();
                yield return null;
            }
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
    }
}