using System;
using UnityEngine;

namespace Player
{
    public class PlayerExperience : MonoBehaviour
    {
        public static PlayerExperience Instance;

        [SerializeField]
        private int _maxLevel;

        [SerializeField]
        private int _baseExp;

        [SerializeField]
        private float _incrementalExp;


        private float _currentTemporalExp;
        private float _nextLevelExp;

        public int Level { get; private set; }
        public float CurrentTemporalExp => _currentTemporalExp;
        public float NextLevelExp => _nextLevelExp;

        public static event Action OnExpGained;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            
            Instance = this;
        }

        private void Start()
        {
            Level = 1;
            _nextLevelExp = _baseExp;
        }

        [ContextMenu("Give Exp")]
        private void GiveExp()
        {
            AddExperience(10);
        }

        public void AddExperience(float exp)
        {
            if(exp <= 0) { return; }

            OnExpGained?.Invoke();
            float expToNextLevel = _nextLevelExp - _currentTemporalExp;

            if(exp >= expToNextLevel)
            {
                exp -= expToNextLevel;
                UpdateLevel();
                AddExperience(exp);
            }
            else
            {
                _currentTemporalExp += exp;
                if(_currentTemporalExp == _nextLevelExp)
                {
                    UpdateLevel();
                }
            }
        }

        private void UpdateLevel()
        {
            if(Level >= _maxLevel) { return; }

            Level ++;
            _currentTemporalExp = 0;
            _nextLevelExp *= _incrementalExp;
        }
    }
}