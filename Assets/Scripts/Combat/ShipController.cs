using Perseus.Ships;
using UnityEngine;

[RequireComponent(typeof(ShipMovement))]
public class ShipController : MonoBehaviour
{
    private ShipMovement ShipMovement;
    private Ship Ship;

    private void Awake()
    {
        Ship = new Ship();

        ShipMovement = GetComponent<ShipMovement>();
        ShipMovement.Initialize(Ship);
    }

    public void OrderShipToTarget(Vector3 destination)
    {
        Ship.SetMoveTarget(destination);
    }

    public float GetFormationOffset()
    {
        return Ship.FormationOffset;
    }
}