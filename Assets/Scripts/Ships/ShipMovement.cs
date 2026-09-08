using UnityEngine;

namespace Perseus.Ships
{
    public class ShipMovement : MonoBehaviour
    {
        private Ship Ship;

        public void Initialize(Ship shipState)
        {
            Ship = shipState;
        }

        private void Update()
        {
            MoveToTarget();
        }

        public void MoveToTarget()
        {
            if (Ship != null && Ship.HasMoveTarget)
            {
                UpdateMovement(transform, Time.deltaTime);
            }
        }

        public void UpdateMovement(Transform shipTransform, float deltaTime)
        {
            if (!Ship.HasMoveTarget)
            {
                return;
            }

            Vector3 directionToTarget = Ship.MoveTarget - shipTransform.position;
            directionToTarget.y = 0f;
            float distance = directionToTarget.magnitude;

            if (directionToTarget.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized, Vector3.up);
                shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation, targetRotation, deltaTime * Ship.TurnSpeed);
            }

            Ship.Speed = Mathf.MoveTowards(Ship.Speed, Ship.MaxSpeed, Ship.Acceleration * deltaTime);

            float step = Ship.Speed * deltaTime;
            float clampedStep = Mathf.Min(step, distance);

            Vector3 nextPosition = Vector3.MoveTowards(shipTransform.position, Ship.MoveTarget, clampedStep);
            nextPosition.y = shipTransform.position.y;
            shipTransform.position = nextPosition;
        }
    }
}