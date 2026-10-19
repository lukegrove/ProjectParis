using UnityEngine;

namespace ProjectParis.Campaign.Parties
{
    public class PartyManager : MonoBehaviour
    {
        public LayerMask CombatPlane;
        private PartyController Party;

        /*private void Awake()
        {
            Party = FindAnyObjectByType<PartyController>();
        }

        private void Update()
        {
            if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                LayerMask mask = CombatPlane.value == 0 ? ~0 : CombatPlane;

                if (Physics.Raycast(ray, out RaycastHit hit, 1000f, mask))
                {
                    OrderPartyToTarget(hit.point);
                }
            }
        }

        public void OrderPartyToTarget(Vector3 destination)
        {
            if (Party != null)
            {
                Party.OrderPartyToTarget(destination);
            }
        }*/
    }
}