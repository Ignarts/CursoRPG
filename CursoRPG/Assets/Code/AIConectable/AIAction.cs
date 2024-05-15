using UnityEngine;

namespace Entities.AI
{
    /// <summary>
    /// Base class for AI actions.
    /// </summary>
    public abstract class AIAction : ScriptableObject
    {
        public abstract void Act(AIController controller);
    }
}
