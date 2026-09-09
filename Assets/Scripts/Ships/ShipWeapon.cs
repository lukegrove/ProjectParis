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

        public IInteractable DetectShipsInRange()
        {
            Collider[] enemyTargets = Physics.OverlapSphere(transform.position, range, TargetLayer);
            
            foreach (var target in enemyTargets)
            {
                return target.GetComponent<IInteractable>();
            }

            return null;
        }

        public bool FireAtTarget(IInteractable target)
        {
            if (target != null && cooldown == 0)
            {
                target.Interact();
                cooldown = 1 / fireRate;
                return true;
            }

            return false;
        }
    }
}