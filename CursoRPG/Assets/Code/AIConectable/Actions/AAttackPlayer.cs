using UnityEngine;

namespace Entities.AI.Actions
{
    /// <summary>
    /// Action to control the attack behavior to the AIController
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Actions/AttackPlayer", fileName = "Action_AttackPlayer")]
    public class AAttackPlayer : AIAction
    {
        /// <summary>
        /// Base Act Method
        /// </summary>
        /// <param name="controller"></param>
        public override void Act(AIController controller)
        {
            AttackPlayer(controller);
        }

        /// <summary>
        /// Attack the player
        /// </summary>
        /// <param name="controller"></param>
        private void AttackPlayer(AIController controller)
        {
            if (controller.Target == null)
                return;
            
            if(!controller.CanAttack())
            {
                return;
            }
            
            if(controller.IsPlayerOnAttackRange() && controller.CanAttack())
            {
                controller.MeleeAttack(controller.Damage);
            }

            controller.UpdateNextAttackTime();
        }
    }
}
