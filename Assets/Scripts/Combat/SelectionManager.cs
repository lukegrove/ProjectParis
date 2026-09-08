using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<ShipController> selectedShips = new();

    public void SelectShip(ShipController ship)
    {
        if (!selectedShips.Contains(ship))
        {
            selectedShips.Add(ship);
        }
    }

    public void UnselectAllShips()
    {
        selectedShips.Clear();
    }

    public bool IsEmpty()
    {
        return selectedShips.Count == 0;
    }
}