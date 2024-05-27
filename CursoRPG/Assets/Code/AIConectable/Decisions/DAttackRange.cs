using UnityEngine;

namespace Entities.AI.Decisions
{
    /// <summary>
    /// Decision to check if the player is on attack range
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Decisions/AttackRange", fileName = "Decision_AttackRange")]
    public class DAttackRange : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            return IsPlayerOnAttackRange(controller);
        }

        private bool IsPlayerOnAttackRange(AIController controller)
        {
            if(controller.Target == null)
                return false;

            float distance = Vector3.Distance(controller.Target.position, controller.Transform.position);

            if(distance < controller.AttackRange)
                return true;

            return  false;
        }
    }
}
