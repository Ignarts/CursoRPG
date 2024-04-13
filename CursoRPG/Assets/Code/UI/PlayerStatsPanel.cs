using System;
using System.Collections;
using Managers;
using Player.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PlayerStatsPanel : MonoBehaviour
    {
        #region Private Attributes

        [Header("Menu Attributes")]
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _restPosition;
        [SerializeField] private Transform _activePosition;
        [SerializeField] private float _speed;

        [Header("Stats Values")]
        [SerializeField] private StatPanelValue _damageStat;
        [SerializeField] private StatPanelValue _defenseStat;
        [SerializeField] private StatPanelValue _speedStat;
        [SerializeField] private StatPanelValue _attackSpeedStat;
        [SerializeField] private StatPanelValue _criticalChanceStat;
        [SerializeField] private StatPanelValue _blockChanceStat;

        private PlayerStats _playerStats;
        private Keyboard keyboard;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            keyboard = Keyboard.current;
            StartCoroutine(DeactivatePanel());
        }

        private void Update()
        {
            if(keyboard.tabKey.wasPressedThisFrame)
            {
                StartCoroutine(ActivatePanel());
            }
            else if(keyboard.tabKey.wasReleasedThisFrame)
            {
                StartCoroutine(DeactivatePanel());
            }
        }

        private void OnDisable()
        {
            StatsManager.OnStatsUpdated -= SetStatValueTexts;
        }

        #endregion

        #region Methods

        public void Configure(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            StatsManager.OnStatsUpdated += SetStatValueTexts;
        }

        private IEnumerator ActivatePanel()
        {
            StopCoroutine(DeactivatePanel());

            while (Vector3.Distance(_transform.position, _activePosition.position) > 0.01f)
            {
                _transform.position = Vector3.Lerp(_transform.position, _activePosition.position, _speed * Time.deltaTime);
                yield return null;
            }
            StatsManager.Instance.SetUpStats();
        }

        private IEnumerator DeactivatePanel()
        {
            StopCoroutine(ActivatePanel());

            while (Vector3.Distance(_transform.position, _restPosition.position) > 0.01f)
            {
                _transform.position = Vector3.Lerp(_transform.position, _restPosition.position, _speed * Time.deltaTime);
                yield return null;
            }
        }


        private void SetStatValueTexts()
        {
            _damageStat.Name.text = "Damage";
            _damageStat.Value.text = _playerStats.Damage.ToString();
            _defenseStat.Name.text = "Defense";
            _defenseStat.Value.text = _playerStats.Defense.ToString();
            _speedStat.Name.text = "Speed";
            _speedStat.Value.text = _playerStats.Speed.ToString();
            _attackSpeedStat.Name.text = "Att. Speed";
            _attackSpeedStat.Value.text = _playerStats.AttackSpeed.ToString();
            _criticalChanceStat.Name.text = "Crit.d";
            _criticalChanceStat.Value.text = _playerStats.CriticalChance.ToString();
            _blockChanceStat.Name.text = "Block";
            _blockChanceStat.Value.text = _playerStats.BlockChance.ToString();
        }

        #endregion
    }

    [Serializable]
    public class StatPanelValue
    {
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Value;
    }
}