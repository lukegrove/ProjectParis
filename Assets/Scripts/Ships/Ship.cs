using UnityEngine;

namespace Perseus.Ships
{
    public enum Faction
    {
        Faction1,
        Faction2
    }

    public class Ship
    {
        public float MaxSpeed { get; private set; } = 5f;
        public float Acceleration { get; private set; } = 2f;
        public float TurnSpeed { get; private set; } = 4f;
        public float Hull { get; private set; } = 100f;
        public float Speed { get; set; } // TODO Make private, create a setter method
        public Vector3 MoveTarget { get; private set; }
        public bool HasMoveTarget { get; private set; }
        public float FormationOffset { get; private set; } = 2f;
        public float SafetyOffset { get; private set; } = 2f;
        public Faction Faction;
        private bool Alive { get; set; } = true;
        public float ScannerRadius { get; set; } = 10f;

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

        public void TakeDamage(float damage)
        {
            Hull -= damage;

            if (Hull <= 0)
            {
                Alive = false;
            }
        }

        public bool IsAlive()
        {
            return Alive;
        }
    }
}