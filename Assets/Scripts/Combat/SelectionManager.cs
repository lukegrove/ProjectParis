using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<ShipController> SelectedShips = new();

    public void SelectShip(ShipController ship)
    {
        if (!SelectedShips.Contains(ship))
        {
            SelectedShips.Add(ship);
        }
    }

    public void UnselectAllShips()
    {
        SelectedShips.Clear();
    }

    public bool IsEmpty()
    {
        return SelectedShips.Count == 0;
    }
}