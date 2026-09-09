using Perseus.Ships;
using UnityEngine;

public enum Faction
{
    Faction1,
    Faction2
}

[RequireComponent(typeof(ShipMovement))]
[RequireComponent(typeof(ShipScanner))]
[RequireComponent(typeof(ShipWeapon))]
public class ShipController : MonoBehaviour
{
    private ShipMovement ShipMovement;
    private ShipScanner ShipScanner;
    private ShipWeapon ShipWeapon;
    private Ship Ship;
    public Faction Faction;
    private Faction EnemyFaction;

    private void Awake()
    {
        Ship = new Ship();

        ShipMovement = GetComponent<ShipMovement>();
        ShipMovement.Initialize(Ship);

        ShipScanner = GetComponent<ShipScanner>();
        ShipScanner.Initialize(Ship);

        ShipWeapon = GetComponent<ShipWeapon>();
        ShipWeapon.Initialize(Ship);

        if (Faction == Faction.Faction1)
        {
            EnemyFaction = Faction.Faction2;
        }
        else
        {
            EnemyFaction = Faction.Faction1;
        }
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
        return ShipScanner.DetectEnemyShips(EnemyFaction);
    }

    public float GetWeaponRange()
    {
        return ShipWeapon.range;
    }

    public ShipController GetEnemyShipInRange()
    {
        return ShipWeapon.DetectShipsInRange(EnemyFaction);
    }

    public bool OrderShipToFire(ShipController target)
    {
        return ShipWeapon.FireAtTarget(target);
    }

    public Faction GetEnemyFaction()
    {
        return EnemyFaction;
    }

    public Faction GetFaction()
    {
        return Faction;
    }

    public void TakeDamage(float damage)
    {
        Ship.TakeDamage(damage);
        Debug.Log($"{name} took {damage} damage! Hull at {Ship.Hull}.");

        if (!Ship.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}