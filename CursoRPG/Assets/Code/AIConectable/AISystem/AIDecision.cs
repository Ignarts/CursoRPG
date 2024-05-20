using UnityEngine;

namespace Entities.AI
{
    /// <summary>
    /// Base class for AI decisions.
    /// </summary>
    public abstract class AIDecision : ScriptableObject
    {
        public abstract bool Decide(AIController controller);
    }
}
