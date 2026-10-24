using ProjectParis.Combat.Battles;
using ProjectParis.Campaign.Parties;
using ProjectParis.Combat.Ships;

namespace ProjectParis.Campaign
{
    public class Encounter
    {
        public Party Initiator;
        public Party Target;
        public BattleResult Result;

        public Encounter(Party initiator, Party target)
        {
            Initiator = initiator;
            Target = target;
        }

        public void Attack(bool isPlayer = false)
        {
            BattleTransfer.StartBattle(Initiator, Target, isPlayer);
        }

        public void AsynchBattle()
        {
            int initiatorWeight = 1;
            int targetWeight = 0;
            Battle battle = new();
            //BattleTransfer.Initialize(Initiator, Target);
            battle.BattleState = BattleState.InProgress;
            
            foreach (Ship ship in Initiator.Ships)
            {
                initiatorWeight += ship.Weight;
                battle.RegisterShip(ship);
            }

            foreach (Ship ship in Target.Ships)
            {
                targetWeight += ship.Weight;
                battle.RegisterShip(ship);
            }

            if (initiatorWeight > targetWeight)
            {
                foreach (Ship ship in Target.Ships)
                {
                    battle.ReportLoss(ship);
                }
            }
            else
            {
                foreach (Ship ship in Initiator.Ships)
                {
                    battle.ReportLoss(ship);
                }
            }
        }
    }
}