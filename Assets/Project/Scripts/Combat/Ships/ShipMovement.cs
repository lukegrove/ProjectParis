using UnityEngine;

namespace ProjectParis.Combat.Ships
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField] private float AvoidanceRadius = 2f;
        [SerializeField] private float AvoidanceStrength = 2f;
        private LayerMask TargetLayer;
        [SerializeField] private float StoppingDistance = 0.2f;

        private Ship Ship;

        public void Initialize(Ship shipState, LayerMask targetLayer)
        {
            Ship = shipState;
            TargetLayer = targetLayer;
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

            Vector3 avoidance = Vector3.zero;

            Collider[] nearbyShips = Physics.OverlapSphere(shipTransform.position, AvoidanceRadius, TargetLayer);
            
            foreach (Collider other in nearbyShips)
            {
                Vector3 away = shipTransform.position - other.transform.position;
                away.y = 0f;

                float distance = away.magnitude;

                if (distance > 0.001f)
                {
                    float weight = 1f - distance / AvoidanceRadius;
                    avoidance += away.normalized * weight;
                }
            }

            Vector3 directionToTarget = Ship.MoveTarget - shipTransform.position;
            directionToTarget.y = 0f;

            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget <= StoppingDistance)
            {
                shipTransform.position = new Vector3(
                    Ship.MoveTarget.x,
                    shipTransform.position.y,
                    Ship.MoveTarget.z);

                Ship.Stop();
                return;
            }

            directionToTarget = directionToTarget.normalized + avoidance * AvoidanceStrength;
            directionToTarget.y = 0f;

            if (directionToTarget.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized, Vector3.up);
                shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation, targetRotation, deltaTime * Ship.TurnSpeed);
            }

            Ship.Speed = Mathf.MoveTowards(Ship.Speed, Ship.MaxSpeed, Ship.Acceleration * deltaTime);

            float step = Ship.Speed * deltaTime;
            float clampedStep = Mathf.Min(step, distanceToTarget);

            //Vector3 nextPosition = Vector3.MoveTowards(shipTransform.position, Ship.MoveTarget, clampedStep);
            Vector3 nextPosition = shipTransform.position + directionToTarget.normalized * clampedStep;
            nextPosition.y = shipTransform.position.y;
            shipTransform.position = nextPosition;
        }
    }
}