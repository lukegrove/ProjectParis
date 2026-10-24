
using UnityEngine.SceneManagement;
using ProjectParis.Campaign.Parties;
using Unity.VisualScripting;

namespace ProjectParis.Combat.Battles
{
    public static class BattleTransfer // Transition?
    {
        public static Party Initiator;
        public static Party Target;
        public static bool IsPlayerInitiator;

        public static void Initialize(Party initiator, Party target, bool isPlayerInitiator = false)
        {
            Initiator = initiator;
            Target = target;
            IsPlayerInitiator = isPlayerInitiator;
        }

        public static void StartBattle(Party initiator, Party target, bool isPlayerInitiator = false)
        {
            Initialize(initiator, target, isPlayerInitiator);
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        }
    }
}