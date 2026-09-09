using UnityEngine;
using UnityEngine.InputSystem;

namespace Perseus.Ships
{
    public class ShipScanner : MonoBehaviour
    {
        public LayerMask TargetLayer;
        private Ship Ship;

        public void Initialize(Ship shipState)
        {
            Ship = shipState;
        }

        public Vector3 DetectEnemyShips(Faction EnemyFaction)
        {
            // Change to use NonAlloc method later
            Collider[] enemyTargets = Physics.OverlapSphere(transform.position, Ship.ScannerRadius, TargetLayer);
            foreach (var target in enemyTargets)
            {
                ShipController shipController = target.GetComponentInParent<ShipController>();

                if (shipController != null && shipController.GetFaction() == EnemyFaction)
                {
                    return target.transform.position;
                }
            }

            return transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            if (Ship == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Ship.ScannerRadius);
        }
    }
}