using UnityEngine;
using ProjectParis.Combat.Ships;

namespace ProjectParis.Campaign.Parties
{
    [RequireComponent(typeof(PartyMovement))]
    public class PartyController : MonoBehaviour
    {
        [System.NonSerialized]
        public Party Party;
        private PartyMovement PartyMovement;
        public Faction Faction;

        void Awake()
        {
            Party = new Party();
            PartyMovement = GetComponent<PartyMovement>();
            PartyMovement.Initialize(Party);

            Party.AddShip();
            Party.AddShip();
            Party.AddShip();

            Party.Faction = Faction;
        }

        public void OrderPartyToTarget(Vector3 destination)
        {
            Party.SetMoveTarget(destination);
        }

        public void OrderPartyToParty(Vector3 destination)
        {
            Party.SetMoveTarget(destination);
        }

        public void RemoveShip(Ship ship)
        {
            Party.RemoveShip(ship);
        }

        public int GetShipCount()
        {
            return Party.Ships.Count;
        }
    }
}