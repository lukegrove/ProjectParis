using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
    None,
    Corvette,
    Destroyer,
    Command
}

namespace ProjectParis.Combat.Ships
{
    public class Ship
    {
        public float MaxSpeed { get; private set; }
        public float Acceleration { get; private set; }
        public float TurnSpeed { get; private set; }
        public float Hull { get; private set; }
        //public float Shields { get; private set; }
        public float Speed { get; private set; }
        public float FormationOffset { get; private set; } = 2f;
        public float SafetyOffset { get; private set; } = 2f;
        public float ScannerRadius { get; private set; } = 10f;
        public float WeaponLimit { get; private set; }
        public bool HasMoveTarget { get; private set; }
        public bool Alive { get; private set; } = true;
        public Vector3 MoveTarget { get; private set; }
        public List<WeaponType> WeaponLoadout { get; private set; }
        public ShipType ShipType { get; private set; }
        public int Weight { get; private set; } = 1;
        public Faction faction;

        protected void SetShipType(ShipType shipType)
        {
            ShipType = shipType;
        }

        protected void SetMaxSpeed(float speed)
        {
            MaxSpeed = speed;
        }

        protected void SetAcceleration(float acceleration)
        {
            Acceleration = acceleration;
        }

        protected void SetTurnSpeed(float turnSpeed)
        {
            TurnSpeed = turnSpeed;
        }

        protected void SetHull(float hull)
        {
            Hull = hull;
        }

        protected void SetWeaponLoadout(List<WeaponType> loadout)
        {
            if (loadout.Count > WeaponLimit)
            {
                Debug.Log($"Weapon loadout exceeds weapon limit for Ship.");
                return;
            }

            WeaponLoadout = loadout;
        }

        public void SetSpeed(float speed)
        {
            if (speed >= MaxSpeed)
            {
                speed = MaxSpeed;
            }

            Speed = speed;
        }

        public void SetMoveTarget(Vector3 destination)
        {
            MoveTarget = new Vector3(destination.x, 0f, destination.z);
            HasMoveTarget = true;
            Speed = 0f;
        }

        public void SetWeaponLimit(float limit)
        {
            WeaponLimit = limit;
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
            /*if (Shields > 0) Needs to be set before this
            {
                Shields-= damage;
                return;
            }*/

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