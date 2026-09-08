using UnityEngine;
using UnityEngine.InputSystem;

namespace Perseus.Ships
{
    public class ShipWeapon : MonoBehaviour
    {
        public LayerMask TargetLayer;

        public float range = 10f;
        public float fireRate = 1f;
        public float cooldown = 0;

        void Start()
        {
            //
        }

        void Update()
        {
            if (cooldown == 0)
            {
                IInteractable target = FindTargetForward();
                
                if (target != null)
                {
                    target.Interact();
                    cooldown = 1 / fireRate;
                }

                target = FindTargetLeft();
                
                if (target != null)
                {
                    target.Interact();
                    cooldown = 1 / fireRate;
                }
            }
            else if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else if (cooldown < 0)
            {
                cooldown = 0;
            }
        }

        private IInteractable FindTargetForward()
        {
            // Rays are an invisible line that takes the starting point and direction.
            // So in this case the transform position and facing forward.
            Ray ray = new(transform.position, transform.forward);

            // Need spherecast + angle filtering
            // Weapon should not always hit, maybe say ineffective attack?
            if (Physics.Raycast(ray, out RaycastHit hit, range, TargetLayer))
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteractable target))
                {
                    return target;
                }
            }

            return null;
        }

        private IInteractable FindTargetLeft()
        {
            Ray ray = new(transform.position, -transform.right);

            if (Physics.Raycast(ray, out RaycastHit hit, range, TargetLayer))
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteractable target))
                {
                    return target;
                }
            }

            return null;
        }
    }
}