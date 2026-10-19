using UnityEngine;
using ProjectParis.Combat.Battles;

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
        private ShipWeapon ShipWeapon;
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

            ShipWeapon = GetComponent<ShipWeapon>();
        }

        public void Initialize(Ship ship, Faction faction, LayerMask targetLayer)
        {
            Ship = ship;
            Faction = faction;
            TargetLayer = targetLayer;
            ShipMovement.Initialize(Ship, TargetLayer);
            ShipScanner.Initialize(Ship, TargetLayer);
            ShipWeapon.Initialize(Ship, TargetLayer);

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

        public float GetWeaponRange()
        {
            return ShipWeapon.range;
        }

        public ShipController GetEnemyShipInRange()
        {
            return ShipWeapon.DetectShipsInRange(EnemyFaction);
        }

        public bool OrderShipToFire(ShipController target)
        {
            return ShipWeapon.FireAtTarget(target);
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
            //Debug.Log($"{name} took {damage} damage! Hull at {Ship.Hull}.");

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