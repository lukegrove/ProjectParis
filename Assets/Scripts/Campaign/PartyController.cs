using UnityEngine;

[RequireComponent(typeof(PartyMovement))]
public class PartyController : MonoBehaviour
{
    private Party Party;
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
}