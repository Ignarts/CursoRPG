using UnityEngine;

namespace Entities.AI.Actions
{
    [CreateAssetMenu(menuName = "AI/Actions/DeactivatePatrol")]
    public class ADeactivatePatrol : AIAction
    {
        #region Base Action Methods
        
        /// <summary>
        /// Activate the patrol
        /// </summary>
        /// <param name="controller"></param>
        public override void Act(AIController controller)
        {
            if(controller.EnemyMovement == null)
                return;

            controller.EnemyMovement.enabled = false;
        }

        #endregion
    }
}
