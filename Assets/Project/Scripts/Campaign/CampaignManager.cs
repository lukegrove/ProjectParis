using UnityEngine;
using ProjectParis.Combat.Ships;
using ProjectParis.Combat.Battles;
using ProjectParis.Campaign.Parties;

namespace ProjectParis.Campaign
{
    public class CampaignManager : MonoBehaviour
    {
        public PartyController PlayerParty;
        private PartyController EncounterParty;

        public void ReportEncounter(PartyController encounterParty)
        {
            EncounterParty = encounterParty;
        }

        public void EndBattle(BattleResult battleResult)
        {
            foreach (Ship ship in battleResult.AllyShipsLost)
            {
                PlayerParty.RemoveShip(ship);
            }

            foreach (Ship ship in battleResult.EnemyShipsLost)
            {
                EncounterParty.RemoveShip(ship);
            }

            if (battleResult.BattleState == BattleState.Victory)
            {
                if (EncounterParty != null && EncounterParty.GetShipCount() == 0)
                {
                    Destroy(EncounterParty.gameObject);
                    EncounterParty = null;
                }
            }
        }
    }
}