using Unity.InferenceEngine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Perseus.Ships
{
    public class ShipScanner : MonoBehaviour
    {
        private LayerMask TargetLayer;
        private Ship Ship;

        public void Initialize(Ship shipState, LayerMask targetLayer)
        {
            Ship = shipState;
            TargetLayer = targetLayer;
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