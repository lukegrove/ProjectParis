using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FleetController))]
[RequireComponent(typeof(SelectionManager))]
public class InputManager : MonoBehaviour
{
    public LayerMask CombatPlane;
    public LayerMask ShipLayer;
    private FleetController FleetController;
    private SelectionManager SelectionManager;
    private Ray Ray;

    private void Awake()
    {
        FleetController = GetComponent<FleetController>();
        SelectionManager = GetComponent<SelectionManager>();
    }

    void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        var keyboard = Keyboard.current;

        if (keyboard.leftShiftKey.isPressed && Mouse.current.leftButton.wasPressedThisFrame)
        {
            UpdateShipSelection(true);
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            UpdateShipSelection(false);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            UpdateFleetOrders();
        }
    }

    public void UpdateShipSelection(bool multiSelect)
    {
        Ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(Ray, out RaycastHit hit, Mathf.Infinity, ShipLayer))
        {
            ShipController ship = hit.collider.gameObject.GetComponentInParent<ShipController>();
            
            if (ship != null)
            {
                if (!multiSelect)
                {
                    SelectionManager.UnselectAllShips();
                }

                SelectionManager.SelectShip(ship);
            }
        }
        else
        {
            SelectionManager.UnselectAllShips();
        }
    }

    public void UpdateFleetOrders()
    {
        if (!SelectionManager.IsEmpty())
        {
            Ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            LayerMask mask = CombatPlane.value == 0 ? ~0 : CombatPlane;

            if (Physics.Raycast(Ray, out RaycastHit hit, 1000f, mask))
            {
                Vector3 moveTarget = hit.point;
                moveTarget.y = transform.position.y;
                FleetController.OrderSelectedShipsToTarget(moveTarget);
            }
        }
    }
}