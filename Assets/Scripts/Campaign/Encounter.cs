using UnityEngine.SceneManagement;

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

    public void Attack()
    {
        BattleTransfer.StartBattle(Initiator, Target);
    }
}