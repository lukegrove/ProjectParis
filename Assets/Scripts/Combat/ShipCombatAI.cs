using Perseus.Ships;
using UnityEngine;

// LostTarget
//     Target was destroyed, disabled, or has been unreachable for a period.
//     Clear it and return to Searching.

// You can implement this with an enum and a single update method initially. 
// Avoid introducing a full behavior-tree system until the basic loop works.

// Need logic to determine what makes a target a valid target
// Maybe if it has a shipcontroller and an identifier, and is alive?

// Faction filter
// Target selection, nearest or lowest health?

// if current target is invalid:
//     find a new target

// if target disappears:
//     clear target and search again

// closer than minimum range: move away or reposition
// The movement destination can become an orbit or formation position.

[RequireComponent(typeof(ShipController))]
public class ShipCombatAI : MonoBehaviour
{
    private const float ScanInterval = 0.5f;
    private float ScanTimer;
    private ShipController ShipController;
    private Ship Ship;
    private Vector3 destination;
    private Vector3 target;
    public bool hasTarget;
    private bool hasDestination;

    private void Awake()
    {
        Ship = new Ship();

        ShipController = GetComponent<ShipController>();
    }

    void Update()
    {
        ScanTimer -= Time.deltaTime;

        if (ScanTimer > 0f)
        {
            return;
        }

        ScanTimer = ScanInterval;

        Vector3 detectedTarget = ShipController.DetectEnemyShips();

        if (detectedTarget != transform.position)
        {
            target = detectedTarget;
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }

        if (hasTarget)
        {
            Vector3 direction = (target - transform.position).normalized;
            Vector3 newDestination = target - direction * (ShipController.GetWeaponRange() - 1f);

            if (!hasDestination ||
                (newDestination - destination).sqrMagnitude > 0.25f)
            {
                destination = newDestination;
                hasDestination = true;
                ShipController.OrderShipToTarget(destination);
            }
        }

        if (hasDestination)
        {
            IInteractable _ = ShipController.GetEnemyShipInRange();
            if (_ != null && ShipController.OrderShipToFire(_))
            {
                Debug.Log("Hit!");
            }
        }
    }
}