using ProjectParis.Combat.Battles;
using ProjectParis.Campaign.Parties;

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
    }
}