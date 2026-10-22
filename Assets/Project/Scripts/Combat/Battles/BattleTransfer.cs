
using UnityEngine.SceneManagement;
using ProjectParis.Campaign.Parties;

namespace ProjectParis.Combat.Battles
{
    public static class BattleTransfer // Transition?
    {
        public static Party Initiator;
        public static Party Target;
        public static bool IsPlayerInitiator;

        public static void StartBattle(Party initiator, Party target, bool isPlayerInitiator = false)
        {
            Initiator = initiator;
            Target = target;
            IsPlayerInitiator = isPlayerInitiator;
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        }
    }
}