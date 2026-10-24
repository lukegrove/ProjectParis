using UnityEngine;

namespace ProjectParis.Combat.Ships
{
    [RequireComponent(typeof(ShipController))]
    public class ShipCombatAI : MonoBehaviour
    {
        private const float ScanInterval = 0.5f;
        private float ScanTimer;
        private ShipController ShipController;
        private Ship Ship;
        private Vector3 destination;
        private Vector3 target;
        public bool hasTarget;
        private bool hasDestination;

        private void Awake()
        {
            Ship = new Ship();

            ShipController = GetComponent<ShipController>();
        }

        void Update()
        {
            ScanTimer -= Time.deltaTime;

            if (ScanTimer > 0f)
            {
                return;
            }

            ScanTimer = ScanInterval;

            Vector3 detectedTarget = ShipController.DetectEnemyShips();

            if (detectedTarget != transform.position)
            {
                target = detectedTarget;
                hasTarget = true;
            }
            else
            {
                hasTarget = false;
            }

            if (hasTarget)
            {
                Vector3 direction = (target - transform.position).normalized;
                float weaponRange = ShipController.GetWeaponRange();
                Vector3 newDestination = target - direction * (weaponRange - 1f);

                if (!hasDestination ||
                    (newDestination - destination).sqrMagnitude > 0.25f)
                {
                    destination = newDestination;
                    hasDestination = true;
                    ShipController.OrderShipToTarget(destination);
                }
            }

            if (hasDestination)
            {
                ShipController _ = ShipController.GetEnemyShipInRange();
                if (_ != null)
                {
                    ShipController.OrderShipToFire(_);
                }
            }
        }
    }
}