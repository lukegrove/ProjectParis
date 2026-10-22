using System.Collections.Generic;
using ProjectParis.Combat.Ships;
using UnityEngine;

namespace ProjectParis.Campaign.Parties
{
    public class Party
    {
        // Character/Lord field
        public List<Ship> Ships = new();
        public Faction Faction;
        public bool HasMoveTarget;
        public Vector3 MoveTarget;

        public void AddShipCorvette()
        {
            Ships.Add(new ShipCorvette());
        }

        public void AddShipDestroyer()
        {
            Ships.Add(new ShipDestroyer());
        }

        public void AddShipCommand()
        {
            Ships.Add(new ShipCommand());
        }

        public void RemoveShip(Ship ship)
        {
            Ships.Remove(ship);
        }

        public void Stop()
        {
            HasMoveTarget = false;
            MoveTarget = Vector3.zero;
        }

        public void SetMoveTarget(Vector3 destination)
        {
            MoveTarget = new Vector3(destination.x, 0f, destination.z);
            HasMoveTarget = true;
        }

        public void FollowParty(PartyController party)
        {
            MoveTarget = party.transform.position;
            HasMoveTarget = true;
        }

    }
}