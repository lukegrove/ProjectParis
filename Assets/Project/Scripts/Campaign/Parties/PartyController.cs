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

            Party.AddShipCorvette();
            Party.AddShipDestroyer();
            Party.AddShipDestroyer();

            Party.Faction = Faction;

            if (Faction == Faction.Faction1)
            {
                Party.AddShipCommand();
            }
            else
            {
                Party.AddShipCorvette();
            }

            PartyMovement = GetComponent<PartyMovement>();
            PartyMovement.Initialize(Party);
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

        public Faction GetFaction()
        {
            return Party.Faction;
        }
    }
}