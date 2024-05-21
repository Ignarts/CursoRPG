using UnityEditor;
using UnityEngine;

namespace Entities.AI
{
    public class AIController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private AIState _startState;

        [Header("Configuration")]
        [SerializeField] private Transform _transform;
        [SerializeField] private float _detectionRange = 10.0f;
        [SerializeField] private LayerMask _detectionMask;
        [SerializeField] private EnemyMovement _enemyMovement;
        
        #endregion

        #region Properties

        public AIState ActualState { get; set; }
        public Transform Target { get; set; }

        public Transform Transform => _transform;
        public float DetectionRange => _detectionRange;
        public LayerMask DetectionMask => _detectionMask;
        public EnemyMovement EnemyMovement => _enemyMovement;
        
        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            ActualState = _startState;
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
        
        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_transform.position, _detectionRange);

            if(Target != null)
            {
                Gizmos.color = Color.green;
                Vector3 direction = Target.position - _transform.position;
                Gizmos.DrawRay(_transform.position, direction);
            }

            if(!Application.isPlaying)
                return;
                
#if UNITY_EDITOR
            Handles.Label(_transform.position + Vector3.down * 1.2f, ActualState.ToString());
#endif
        }
        
        #endregion
    }
}
