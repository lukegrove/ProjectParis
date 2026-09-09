using UnityEngine;
using UnityEngine.InputSystem;

namespace Perseus.Ships
{
    public class ShipWeapon : MonoBehaviour
    {
        public LayerMask TargetLayer;

        public float range = 5f;
        public float fireRate = 1f;
        public float cooldown = 0;
        public float damage = 1;

        private Ship Ship;

        public void Initialize(Ship shipState)
        {
            Ship = shipState;
        }

        void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else if (cooldown < 0)
            {
                cooldown = 0;
            }
        }

        public ShipController DetectShipsInRange(Faction EnemyFaction)
        {
            Collider[] enemyTargets = Physics.OverlapSphere(transform.position, range, TargetLayer);
            
            foreach (var target in enemyTargets)
            {
                ShipController shipController = target.GetComponentInParent<ShipController>();

                if (shipController != null && shipController.GetFaction() == EnemyFaction)
                {
                    return shipController;
                }
            }

            return null;
        }

        public bool FireAtTarget(ShipController target)
        {
            if (target != null && cooldown == 0)
            {
                target.TakeDamage(damage);
                cooldown = 1 / fireRate;
                return true;
            }

            return false;
        }
    }
}