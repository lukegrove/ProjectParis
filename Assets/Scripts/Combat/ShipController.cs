using Perseus.Ships;
using UnityEngine;

[RequireComponent(typeof(ShipMovement))]
[RequireComponent(typeof(ShipScanner))]
[RequireComponent(typeof(ShipWeapon))]
public class ShipController : MonoBehaviour
{
    private ShipMovement ShipMovement;
    private ShipScanner ShipScanner;
    private ShipWeapon ShipWeapon;
    private Ship Ship;

    private void Awake()
    {
        Ship = new Ship();

        ShipMovement = GetComponent<ShipMovement>();
        ShipMovement.Initialize(Ship);

        ShipScanner = GetComponent<ShipScanner>();
        ShipScanner.Initialize(Ship);

        ShipWeapon = GetComponent<ShipWeapon>();
        ShipWeapon.Initialize(Ship);
    }

    public void OrderShipToTarget(Vector3 destination)
    {
        Ship.SetMoveTarget(destination);
    }

    public float GetFormationOffset()
    {
        return Ship.FormationOffset;
    }

    public Vector3 DetectEnemyShips()
    {
        return ShipScanner.DetectEnemyShips();
    }

    public float GetWeaponRange()
    {
        return ShipWeapon.range;
    }

    public IInteractable GetEnemyShipInRange()
    {
        return ShipWeapon.DetectShipsInRange();
    }

    public bool OrderShipToFire(IInteractable target)
    {
        return ShipWeapon.FireAtTarget(target);
    }
}