using UnityEngine;

namespace Entities.AI
{
    [CreateAssetMenu(fileName = "NewAIState", menuName = "AI/State")]
    public class AIState : ScriptableObject
    {
        #region Public Attributes

        [SerializeField] private AIAction[] _actions;
        [Space(10)]
        [SerializeField] private AIConnector[] _connectors;
        
        #endregion

        #region Properties

        public AIAction[] Actions => _actions;
        
        #endregion

        #region Methods

        public void ExecuteState(AIController controller)
        {
            ExecuteActions(controller);
            ExecuteConnections(controller);
        }

        /// <summary>
        /// Update the state
        /// </summary>
        /// <param name="controller"></param>
        public void ExecuteActions(AIController controller)
        {
            foreach (var action in _actions)
            {
                action.Act(controller);
            }
        }

        /// <summary>
        /// Execute the connections of the state
        /// </summary>
        /// <param name="controller"></param>
        public void ExecuteConnections(AIController controller)
        {
            AIState stateToSet = null;
            AIState currentState = controller.ActualState;
        
            foreach (var connector in _connectors)
            {
                if (connector.decision.Decide(controller))
                {
                    if (connector.trueState != currentState)
                    {
                        stateToSet = connector.trueState;
                        break;
                    }
                }
                else if (stateToSet == null && connector.falseState != currentState)
                {
                    stateToSet = connector.falseState;
                }
            }
        
            if (stateToSet != null)
            {
                controller.ChangeState(stateToSet);
            }
        }

        #endregion
    }
}
