using UnityEngine;

namespace Entities.AI
{
    public class AIController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private AIState _startState;
        
        #endregion

        #region Properties

        public AIState ActualState { get; set; }
        
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
    }
}
