using UnityEngine;
using ProjectParis.Combat.Battles;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public enum Faction
{
    Faction1,
    Faction2
}

namespace ProjectParis.Combat.Ships
{
    [RequireComponent(typeof(ShipMovement))]
    [RequireComponent(typeof(ShipScanner))]
    [RequireComponent(typeof(ShipWeapon))]
    public class ShipController : MonoBehaviour
    {
        private ShipMovement ShipMovement;
        private ShipScanner ShipScanner;
        //private ShipWeapon ShipWeapon;
        private ShipWeapon[] ShipWeapons;
        private Ship Ship;
        public Faction Faction;
        private Faction EnemyFaction;
        private LayerMask TargetLayer;
        private BattleSession BattleSession;

        private void Awake()
        {
            BattleSession = FindAnyObjectByType<BattleSession>();

            ShipMovement = GetComponent<ShipMovement>();

            ShipScanner = GetComponent<ShipScanner>();

            ShipWeapons = GetComponents<ShipWeapon>();
        }

        public void Initialize(Ship ship, Faction faction, LayerMask targetLayer)
        {
            Ship = ship;
            Faction = faction;
            TargetLayer = targetLayer;
            ShipMovement.Initialize(Ship, TargetLayer);
            ShipScanner.Initialize(Ship, TargetLayer);

            if (ship.WeaponLoadout.Count <= ship.WeaponLimit)
            {
                for (int i = 0; i < ship.WeaponLoadout.Count; i++)
                {
                    ShipWeapons[i].Initialize(ship.WeaponLoadout[i], TargetLayer);
                }
            }

            if (Faction == Faction.Faction1)
            {
                EnemyFaction = Faction.Faction2;
            }
            else
            {
                EnemyFaction = Faction.Faction1;
            }
        }

        public void OrderShipToTarget(Vector3 destination)
        {
            Ship.SetMoveTarget(destination);
        }

        public float GetFormationOffset()
        {
            return Ship.FormationOffset;
        }

        public Vector3 DetectEnemyShips()
        {
            return ShipScanner.DetectEnemyShips(EnemyFaction);
        }

        /*public ShipWeapon[] GetShipWeapons()
        {
            return ShipWeapons;
        }*/

        public float GetWeaponRange()
        {
            float weaponRange = 0f;
            foreach (WeaponType weapon in Ship.WeaponLoadout)
            {
                weaponRange = Mathf.Max(weaponRange, weapon.Range);
            }

            return weaponRange;
        }

        public ShipController GetEnemyShipInRange()
        {
            foreach (ShipWeapon weapon in ShipWeapons)
            {
                ShipController target = weapon.DetectShipsInRange(EnemyFaction);

                if (target != null)
                {
                    return target;
                }
            }

            return null;
        }

        public bool OrderShipToFire(ShipController target)
        {
            bool hit = false;

            foreach (ShipWeapon weapon in ShipWeapons)
            {
                hit = hit || weapon.FireAtTarget(target);
            }

            return hit;
        }

        public Faction GetEnemyFaction()
        {
            return EnemyFaction;
        }

        public Faction GetFaction()
        {
            return Faction;
        }

        public void TakeDamage(float damage)
        {
            Ship.TakeDamage(damage);

            if (!Ship.IsAlive())
            {
                BattleSession.ReportLoss(this);
                Destroy(gameObject);
            }
        }

        public Ship GetShip()
        {
            return Ship;
        }
    }
}