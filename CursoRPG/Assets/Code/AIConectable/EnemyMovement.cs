using UnityEngine;

namespace Entities.AI
{
    public class EnemyMovement : WaypointMovement
    {
        protected override void MoveEntity()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, NextWaypointPoint, _speed * Time.deltaTime);

            if (_transform.position == NextWaypointPoint)
            {
                _actualPointIndex = (_actualPointIndex + 1) % _waypoint.Points.Length;
                _movementState = MovementState.Waiting;
                _isOnPoint = true;
                return;
            }
        }
    }
}
