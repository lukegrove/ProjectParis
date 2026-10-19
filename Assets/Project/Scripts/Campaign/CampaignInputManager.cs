using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PartyController))]
public class CampaignInputManager : MonoBehaviour
{
    public CampaignManager campaignManager;
    public LayerMask CombatPlane;
    public LayerMask ShipLayer;
    private PartyController PlayerParty, TargetParty;
    private Ray Ray;
    private bool PendingEncounter = false;

    private void Awake()
    {
        PlayerParty = GetComponent<PartyController>();
    }

    void Update()
    {
        if (PendingEncounter && TargetParty != null)
        {
            float dist = Vector3.Distance(PlayerParty.transform.position, TargetParty.transform.position);

            if (PendingEncounter && dist <= 1f)
            {
                Encounter encounter = new(PlayerParty.Party, TargetParty.Party);
                PendingEncounter = false;
                encounter.Attack();
                campaignManager.ReportEncounter(TargetParty);
            }
        }

        if (Mouse.current == null)
        {
            return;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            UpdatePartyOrders();
        }
    }

    public void UpdatePartyOrders()
    {
        Ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(Ray, out RaycastHit hit, 1000f, ~0))
        {
            TargetParty = hit.collider.GetComponentInParent<PartyController>();

            if (TargetParty != null && TargetParty != PlayerParty)
            {
                PlayerParty.OrderPartyToParty(hit.point);
                PendingEncounter = true;
                Debug.Log("Party time");
                return;
            }
        }

        LayerMask mask = CombatPlane.value == 0 ? ~0 : CombatPlane;

        if (Physics.Raycast(Ray, out hit, 1000f, mask))
        {
            Vector3 moveTarget = hit.point;
            moveTarget.y = PlayerParty.transform.position.y;
            PlayerParty.OrderPartyToTarget(moveTarget);
        }
    }
}