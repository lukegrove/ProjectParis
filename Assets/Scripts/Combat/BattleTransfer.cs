
using UnityEngine.SceneManagement;

public static class BattleTransfer
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