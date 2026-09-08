using UnityEngine;

[RequireComponent(typeof(SelectionManager))]
public class FleetController : MonoBehaviour
{
    public LayerMask CombatPlane;
    private SelectionManager SelectionManager;

    private void Awake()
    {
        SelectionManager = GetComponent<SelectionManager>();
    }

    public void OrderSelectedShipsToTarget(Vector3 destination)
    {
        foreach (ShipController ship in SelectionManager.selectedShips)
        {
            ship.OrderShipToTarget(destination); // Add offset arg?
        }
    }
}