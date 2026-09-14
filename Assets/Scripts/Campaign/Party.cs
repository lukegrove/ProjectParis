using System.Collections.Generic;
using Perseus.Ships;
using UnityEngine;

public class Party
{
    // Character/Lord field
    public List<Ship> Ships = new();
    public Faction Faction;
    public bool HasMoveTarget;
    public Vector3 MoveTarget;

    public void AddShip()
    {
        Ships.Add(new Ship());
    }

    // Method to add ships

    // Method to update with losses

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