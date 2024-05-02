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

        [Header("Attributes Values")]
        [SerializeField] private TextMeshProUGUI _availablePointsText;
        [SerializeField] private StatPanelValue _strengthStat;
        [SerializeField] private StatPanelValue _intelligenceStat;
        [SerializeField] private StatPanelValue _dexterityStat;

        private PlayerStats _playerStats;
        private Transform _panelPosition;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _panelPosition = _restPosition;
            StartCoroutine(MovePanel());
        }

        private void Update()
        {
            if(Keyboard.current.escapeKey.wasPressedThisFrame && _panelPosition != _restPosition)
            {
                ToggleStatsPanel();
            }
        }

        private void OnDisable()
        {
            StatsManager.OnStatsUpdated -= SetStatValueTexts;
            StatsManager.OnStatsUpdated -= SetAttributesTexts;
        }

        #endregion

        #region Methods

        public void Configure(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            StatsManager.OnStatsUpdated += SetStatValueTexts;
            StatsManager.OnStatsUpdated += SetAttributesTexts;
        }

        public void ToggleStatsPanel()
        {
            _panelPosition = _panelPosition == _restPosition ? _activePosition : _restPosition;
            StartCoroutine(MovePanel());
        }

        private IEnumerator MovePanel()
        {
            while (Vector3.Distance(_transform.position, _panelPosition.position) > 0.1f)
            {
                _transform.position = Vector3.Lerp(_transform.position, _panelPosition.position, _speed * Time.deltaTime);
                yield return null;
            }
        }

        private void SetStatValueTexts()
        {
            _damageStat.Name.text = "Damage";
            _damageStat.Value.text = _playerStats.Damage.ToString("F1");
            _defenseStat.Name.text = "Defense";
            _defenseStat.Value.text = _playerStats.Defense.ToString("F1");
            _speedStat.Name.text = "Speed";
            _speedStat.Value.text = _playerStats.Speed.ToString("F1");
            _attackSpeedStat.Name.text = "Att.Speed";
            _attackSpeedStat.Value.text = _playerStats.AttackSpeed.ToString("F1");
            _criticalChanceStat.Name.text = "Crit.d";
            _criticalChanceStat.Value.text = _playerStats.CriticalChance.ToString("F1");
            _blockChanceStat.Name.text = "Block";
            _blockChanceStat.Value.text = _playerStats.BlockChance.ToString("F1");
        }

        private void SetAttributesTexts()
        {
            _availablePointsText.text = _playerStats.AvailablePoints.ToString();
            _strengthStat.Name.text = "Strength";
            _strengthStat.Value.text = _playerStats.Strength.ToString();
            _intelligenceStat.Name.text = "Intelligence";
            _intelligenceStat.Value.text = _playerStats.Intelligence.ToString();
            _dexterityStat.Name.text = "Dexterity";
            _dexterityStat.Value.text = _playerStats.Dexterity.ToString();
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