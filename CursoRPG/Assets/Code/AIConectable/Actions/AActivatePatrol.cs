using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI.Actions
{
    [CreateAssetMenu(menuName = "AI/Actions/ActivatePatrol")]
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
