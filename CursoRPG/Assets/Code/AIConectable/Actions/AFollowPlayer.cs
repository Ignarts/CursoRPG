using UnityEngine;

namespace Entities.AI.Actions
{
    [CreateAssetMenu(menuName = "AI/Actions/FollowPlayer")]
    public class AFollowPlayer : AIAction
    {
        #region Const

        private const float DISTANCE_TO_STOP = 1.15f;
        
        #endregion
        
        #region Base Action Methods

        /// <summary>
        /// Base Act method
        /// </summary>
        /// <param name="controller"></param>
        public override void Act(AIController controller)
        {
            FollowPlayer(controller);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Follow the playerS
        /// </summary>
        /// <param name="controller"></param>
        private void FollowPlayer(AIController controller)
        {
            if(controller.Target == null) 
                return;

            Vector3 directionToTarget = controller.Target.position - controller.Transform.position;
            Vector3 direction = directionToTarget.normalized;
            float distance = directionToTarget.magnitude;

            if(distance >= DISTANCE_TO_STOP)
            {
                controller.transform.Translate(direction * controller.EnemyMovement.Speed * Time.deltaTime);
            }
        }

        #endregion
    }
}
