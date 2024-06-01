using System.Collections;
using Managers;
using Player;
using Player.Scriptables;
using UnityEditor;
using UnityEngine;

namespace Entities.AI
{
    public enum AttackTypes
    {
        Melee,
        Range
    }

    public class AIController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private AIState _startState;

        [Header("Configuration")]
        [SerializeField] private Transform _transform;
        [SerializeField] private float _detectionRange = 10.0f;
        [SerializeField] private LayerMask _detectionMask;
        [SerializeField] private EnemyMovement _enemyMovement;

        [Header("Attacks")]
        [SerializeField] private AttackTypes _attackType = AttackTypes.Melee;
        [SerializeField] private float _damage = 1.0f;
        [SerializeField] private float _attackSpeed = 1.0f;
        [SerializeField] private float _attackRange = 1.0f;
        [SerializeField] private float _rammingRange = 1.0f;
        [SerializeField] private float _rammingSpeed = 1.0f;

        private float _nextAttackTime = 0.0f;

        private PlayerStats _playerStats;
        private PlayerLife _playerLife;
        private bool isRamming = false;
        
        #endregion

        #region Properties

        public AIState ActualState { get; set; }
        public Transform Target { get; set; }

        public Transform Transform => _transform;
        public float DetectionRange => _detectionRange;
        public LayerMask DetectionMask => _detectionMask;
        public EnemyMovement EnemyMovement => _enemyMovement;

        public AttackTypes AttackType => _attackType;
        public float Damage => _damage;
        public float AttackSpeed => _attackSpeed;

        public float AttackRangeSelected
        {
            get
            {
                return _attackType == AttackTypes.Melee ? _attackRange : _rammingRange;
            }
        }

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            ActualState = _startState;
            _playerStats = StatsManager.Instance.PlayerStats;
            _playerLife = StatsManager.Instance.PlayerLife;
        }

        private void Update()
        {
            ActualState.ExecuteState(this);
        }
        
        #endregion

        #region Methods

        public void ChangeState(AIState newState)
        {
            ActualState = newState;
        }

        /// <summary>
        /// Check if the AI can attack
        /// </summary>
        /// <returns></returns>
        public bool CanAttack()
        {
            return Time.time >= _nextAttackTime;
        }

        /// <summary>
        /// Update the next attack time
        /// </summary>
        public void UpdateNextAttackTime()
        {
            _nextAttackTime = Time.time + _attackSpeed;
        }

        /// <summary>
        /// Check if the player is on attack range
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool IsPlayerOnAttackRange()
        {
            float distance = (Target.position - _transform.position).magnitude;

            if(distance < Mathf.Pow(_attackRange, 2))
                return true;

            return false;
        }

        /// <summary>
        /// Melee Attack Logic
        /// </summary>
        /// <param name="damage"></param>
        public void MeleeAttack(float damage)
        {
            DamagePlayer(damage);
        }

        /// <summary>
        /// Ramming Attack Logic
        /// </summary>
        /// <param name="damage"></param>
        public void RammingAttack(float damage)
        {
            if(!isRamming)
                StartCoroutine(RammingAttackCoroutine(damage));
        }

        /// <summary>
        /// Ramming Attack Logic with Coroutine
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        private IEnumerator RammingAttackCoroutine(float damage)
        {
            isRamming = true;

            // get the target position and the start position
            Vector3 targetPosition = Target.position;
            Vector3 startPosition = _transform.position;

            // calculate the direction and the attack position
            Vector3 direction = (targetPosition - startPosition).normalized;
            Vector3 attackPosition = startPosition + direction * 0.5f;

            // move the AI to the attack position
            float transitionAttack = 0.0f;
            while(transitionAttack <= 1.0f)
            {
                transitionAttack += Time.deltaTime * _rammingSpeed;
                _transform.position = Vector3.Lerp(startPosition, attackPosition, transitionAttack);
                yield return null;
            }

            // damage the player
            if(Target != null)
                DamagePlayer(damage);
            
            isRamming = false;
        }

        /// <summary>
        /// Damage the player
        /// </summary>
        private void DamagePlayer(float damage)
        {
            float damageToDeal = 0;
            float randomValue = Random.Range(0.0f, 1.0f);

            // check if the player block the attack
            if(randomValue < _playerStats.BlockChance / 100)
            {
                Debug.Log("Player dodged the attack");
                return;
            }

            // calculate the damage to deal and apply it
            damageToDeal = Mathf.Max(damage - _playerStats.Defense, 1);
            _playerLife.TakeDamage(damageToDeal);
            _nextAttackTime = 0;
        }
        
        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            DrawAreasGizmos();

            DrawRayGizmos();

            if(!Application.isPlaying)
                return;
                
#if UNITY_EDITOR
            Handles.Label(_transform.position + Vector3.down * 1.2f, ActualState.ToString());
#endif
        }

        private void DrawAreasGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_transform.position, _detectionRange);

            if(_attackType == AttackTypes.Melee)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(_transform.position, _attackRange);
            }

            if(_attackType == AttackTypes.Range)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_transform.position, _rammingRange);
            }
        }

        private void DrawRayGizmos()
        {
            if(Target == null)
                return;

            Gizmos.color = Color.green;
            Vector3 direction = Target.position - _transform.position;
            Gizmos.DrawRay(_transform.position, direction);
        }
        
        #endregion
    }
}
