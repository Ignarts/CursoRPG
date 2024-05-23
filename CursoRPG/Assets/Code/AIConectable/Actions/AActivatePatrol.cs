using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI.Actions
{
    /// <summary>
    /// Action to control the patrol behavior to the AIController
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Actions/ActivatePatrol", fileName = "Action_ActivatePatrol")]
    public class AActivatePatrol : AIAction
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

            controller.EnemyMovement.enabled = true;
        }

        #endregion
    }
}
