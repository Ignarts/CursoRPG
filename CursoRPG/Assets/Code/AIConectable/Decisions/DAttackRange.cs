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

            float distance = (controller.Target.position - controller.Transform.position).magnitude;

            if(distance < Mathf.Pow(controller.AttackRange, 2))
                return true;

            return  false;
        }
    }
}
