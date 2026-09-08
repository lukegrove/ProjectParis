using UnityEngine;
using UnityEngine.InputSystem;

namespace Perseus.Ships
{
    public class ShipMovement : MonoBehaviour
    {
        private Ship ship;
        public LayerMask CombatPlane;

        private void Start()
        {
            ship = new Ship();
        }

        private void Update()
        {
            if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                LayerMask mask = CombatPlane.value == 0 ? ~0 : CombatPlane;

                if (Physics.Raycast(ray, out RaycastHit hit, 1000f, mask, QueryTriggerInteraction.Ignore))
                {
                    Vector3 moveTarget = hit.point;
                    moveTarget.y = transform.position.y;
                    ship.SetMoveTarget(moveTarget);
                }
            }

            if (ship.HasMoveTarget)
            {
                ship.UpdateMovement(transform, Time.deltaTime);
            }
        }
    }
}