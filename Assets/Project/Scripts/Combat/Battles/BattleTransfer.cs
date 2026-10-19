
using UnityEngine.SceneManagement;
using ProjectParis.Campaign.Parties;

namespace ProjectParis.Combat.Battles
{
    public static class BattleTransfer // Transition?
    {
        public static Party Initiator;
        public static Party Target;

        public static void StartBattle(Party initiator, Party target)
        {
            Initiator = initiator;
            Target = target;
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        }
    }
}