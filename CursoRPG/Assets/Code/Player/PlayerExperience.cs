using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerExperience : MonoBehaviour
    {
#region Private Attributes

        [SerializeField]
        private int _maxLevel;

        [SerializeField]
        private int _baseExp;

        [SerializeField]
        private float _incrementalExp;

        private float _currentTemporalExp;
        private float _nextLevelExp;

#endregion

#region Public Attributes

        public static event Action OnExpGained;

#endregion

#region Properties

        public int Level { get; private set; }
        public float CurrentTemporalExp => _currentTemporalExp;
        public float NextLevelExp => _nextLevelExp;

#endregion

#region MonoBehaviour Methods

        private void Start()
        {
            Level = 1;
            _nextLevelExp = _baseExp;
            _currentTemporalExp = 0;
        }

        private void Update() {
            if(Keyboard.current.pKey.wasPressedThisFrame)
            {
                AddExperience(1);
            }
        }

#endregion

#region Methods

        public void AddExperience(float exp)
        {
            if (exp <= 0) { return; }

            float expToNextLevel = _nextLevelExp - _currentTemporalExp;

            if (exp >= expToNextLevel)
            {
                exp -= expToNextLevel;
                UpdateLevel();
                AddExperience(exp);
            }
            else
            {
                _currentTemporalExp += exp;
            }

            OnExpGained?.Invoke();
        }

        private void UpdateLevel()
        {
            if(Level >= _maxLevel) { return; }

            Level ++;
            _currentTemporalExp = 0;
            _nextLevelExp *= _incrementalExp;
        }
#endregion
    }
}