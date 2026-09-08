using UnityEditor;
using UnityEngine;

namespace Perseus.Ships
{
    public class Ship
    {
        public float MaxSpeed { get; private set; } = 5f;
        public float Acceleration { get; private set; } = 2f;
        public float TurnSpeed { get; private set; } = 4f;
        public float Hull { get; private set; } = 100f;
        public float Speed { get; private set; }
        public Vector3 MoveTarget { get; private set; }
        public bool HasMoveTarget { get; private set; }

        public void SetMoveTarget(Vector3 destination)
        {
            MoveTarget = new Vector3(destination.x, 0f, destination.z);
            HasMoveTarget = true;
            Speed = 0f;
        }

        public void Accelerate(float magnitude)
        {
            Speed = Mathf.Min(Speed + magnitude, MaxSpeed);
        }

        public void Stop()
        {
            Speed = 0f;
            HasMoveTarget = false;
            MoveTarget = Vector3.zero;
        }

        public void UpdateMovement(Transform shipTransform, float deltaTime)
        {
            if (!HasMoveTarget)
            {
                return;
            }

            Vector3 directionToTarget = MoveTarget - shipTransform.position;
            directionToTarget.y = 0f;
            float distance = directionToTarget.magnitude;

            if (directionToTarget.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized, Vector3.up);
                shipTransform.rotation = Quaternion.Slerp(shipTransform.rotation, targetRotation, deltaTime * TurnSpeed);
            }

            Speed = Mathf.MoveTowards(Speed, MaxSpeed, Acceleration * deltaTime);

            float step = Speed * deltaTime;
            float clampedStep = Mathf.Min(step, distance);

            Vector3 nextPosition = Vector3.MoveTowards(shipTransform.position, MoveTarget, clampedStep);
            nextPosition.y = shipTransform.position.y;
            shipTransform.position = nextPosition;
        }

        public void TakeDamage(float damage)
        {
            Hull -= damage;
        }
    }
}