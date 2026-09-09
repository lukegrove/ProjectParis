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
        if (!SelectionManager.IsEmpty())
        {
            int shipCount = SelectionManager.SelectedShips.Count;
            
            int colCount = Mathf.CeilToInt(Mathf.Sqrt(shipCount));
            int rowCount = Mathf.CeilToInt(shipCount / colCount);

            for (int i = 0; i < shipCount; i++)
            {
                ShipController ship = SelectionManager.SelectedShips[i];

                int column = i % colCount;
                int row = i / colCount;
                float xOffset = column - (colCount - 1) * 0.5f;
                float zOffset = row - (rowCount - 1) * 0.5f;
                Vector3 offset = new Vector3(xOffset, 0, zOffset) * ship.GetFormationOffset();
                Vector3 offsetDestination = destination + offset;

                //Debug.Log($"{ship.name} target: {offsetDestination}");
                ship.OrderShipToTarget(offsetDestination);
            }
        }
    }
}