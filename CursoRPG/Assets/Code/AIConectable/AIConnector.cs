using System;

namespace Entities.AI
{
    /// <summary>
    /// Base class for AI connectors. Control the flow of the AI.
    /// </summary>
    [Serializable]
    public class AIConnector
    {
        public AIDecision decision;
        public AIState trueState;
        public AIState falseState;
    }
}
