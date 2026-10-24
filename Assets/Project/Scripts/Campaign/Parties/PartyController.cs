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
        private PartySizeView PartySizeView;
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

            PartySizeView = GetComponentInChildren<PartySizeView>();
            PartySizeView.Initialize(this);
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
            if (PartySizeView != null)
            {
                PartySizeView.RefreshCount();
            }
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