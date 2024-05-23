using UnityEngine;

namespace Entities.AI.Decisions
{
    /// <summary>
    /// Decision to check if the player is chase range
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Decisions/FollowPlayer", fileName = "Decision_FollowPlayer")]
    public class DFollowPlayer : AIDecision
    {        
        #region Base Decision Methods

        /// <summary>
        /// Base Decide method
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public override bool Decide(AIController controller)
        {
            return CheckPlayerInRange(controller);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check if the player is in range
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        private bool CheckPlayerInRange(AIController controller)
        {
            Collider2D playerDetected = Physics2D.OverlapCircle(controller.Transform.position, controller.DetectionRange, controller.DetectionMask);

            if(playerDetected != null)
            {
                controller.Target = playerDetected.transform;
                return true;
            }

            controller.Target = null;
            return false;
        }
        
        #endregion
    }
}
