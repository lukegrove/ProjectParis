using UnityEngine;

namespace ProjectParis.Combat.Ships
{
    public class ShipWeapon : MonoBehaviour
    {
        private WeaponType WeaponType;
        private LayerMask TargetLayer;
        private float WeaponCooldown;

        public void Initialize(WeaponType weaponType, LayerMask targetLayer)
        {
            WeaponType = weaponType;
            TargetLayer = targetLayer;
        }

        void Update()
        {
            if (WeaponCooldown > 0)
            {
                WeaponCooldown -= Time.deltaTime;
            }
            else if (WeaponCooldown < 0)
            {
                WeaponCooldown = 0;
            }
        }

        public ShipController DetectShipsInRange(Faction EnemyFaction)
        {
            Collider[] enemyTargets = Physics.OverlapSphere(transform.position, WeaponType.Range, TargetLayer);
            
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
            float weaponDamage = WeaponType.Damage;

            if (target != null && WeaponCooldown == 0)
            {
                target.TakeDamage(weaponDamage);
                Debug.Log($"{name} dealt {weaponDamage} to {target.name}.");
                WeaponCooldown = 1 / WeaponType.FireRate;
                return true;
            }

            return false;
        }
    }
}