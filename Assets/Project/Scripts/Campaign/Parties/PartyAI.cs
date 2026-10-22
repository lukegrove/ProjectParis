using UnityEngine;

namespace ProjectParis.Campaign.Parties
{
    [RequireComponent(typeof(PartyController))]
    public class PartyAI : MonoBehaviour
    {
        private const float ScanInterval = 2f;
        private float ScanTimer;
        private PartyController PartyController;
        private Vector3 destination;
        private Vector3 target;
        public bool hasTarget;
        private bool hasDestination;
        public LayerMask TargetLayer;

        private void Awake()
        {
            PartyController = GetComponent<PartyController>();
        }

        void Update()
        {
            ScanTimer -= Time.deltaTime;

            if (ScanTimer > 0f)
            {
                return;
            }

            ScanTimer = ScanInterval;
            
            Vector3 detectedTarget = DetectEnemyParty();

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
                Vector3 newDestination = target - direction;

                if (!hasDestination ||
                    (newDestination - destination).sqrMagnitude > 0.25f)
                {
                    destination = newDestination;
                    hasDestination = true;
                    PartyController.OrderPartyToTarget(destination);
                }
            }
            else
            {
                if (!PartyController.Party.HasMoveTarget)
                {
                    Vector2 randomOffset = Random.insideUnitCircle * 2f;
                    destination = transform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
                    hasDestination = true;
                    PartyController.OrderPartyToTarget(destination);
                }
            }
        }

        public Vector3 DetectEnemyParty()
        {
            // Change to use NonAlloc method later
            Collider[] enemyTargets = Physics.OverlapSphere(transform.position, 3, TargetLayer);
            foreach (var target in enemyTargets)
            {
                PartyController targetParty = target.GetComponentInParent<PartyController>();

                if (targetParty != null && PartyController.GetFaction() != targetParty.GetFaction())
                {
                    if (targetParty.GetShipCount() <= PartyController.GetShipCount())
                    {
                        Vector3 direction = (target.transform.position - transform.position).normalized;
                        Vector3 newDestination = target.transform.position + direction;
                        return newDestination;
                    }
                    else
                    {
                        Vector3 awayDirection = (transform.position - targetParty.transform.position).normalized;
                        return transform.position + awayDirection * 5f;
                    }
                }
            }

            return transform.position;
        }
    }
}