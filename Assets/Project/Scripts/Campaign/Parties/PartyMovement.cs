using UnityEngine;

namespace ProjectParis.Campaign.Parties
{
    public class PartyMovement : MonoBehaviour
    {
        private Party Party;
        public float Speed;

        public void Initialize(Party party)
        {
            Party = party;
            Speed = 1 + party.Ships.Count;
        }

        private void Update()
        {
            MoveToTarget();
        }

        public void MoveToTarget()
        {
            if (Party != null && Party.HasMoveTarget)
            {
                UpdateMovement(transform, Time.deltaTime);
            }
        }

        public void UpdateMovement(Transform shipTransform, float deltaTime)
        {
            if (!Party.HasMoveTarget)
            {
                return;
            }

            Vector3 directionToTarget = Party.MoveTarget - shipTransform.position;
            directionToTarget.y = 0f;

            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget <= 0)
            {
                shipTransform.position = new Vector3(
                    Party.MoveTarget.x,
                    shipTransform.position.y,
                    Party.MoveTarget.z);

                Party.Stop();
                return;
            }

            directionToTarget = directionToTarget.normalized;
            directionToTarget.y = 0f;

            if (directionToTarget.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized, Vector3.up);
                shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation, targetRotation, deltaTime * 100f);
            }

            float speed = Mathf.MoveTowards(Speed, 10, 1 * deltaTime);

            float step = speed * deltaTime;
            float clampedStep = Mathf.Min(step, distanceToTarget);

            //Vector3 nextPosition = Vector3.MoveTowards(shipTransform.position, Ship.MoveTarget, clampedStep);
            Vector3 nextPosition = shipTransform.position + directionToTarget.normalized * clampedStep;
            nextPosition.y = shipTransform.position.y;
            shipTransform.position = nextPosition;
        }
    }
}