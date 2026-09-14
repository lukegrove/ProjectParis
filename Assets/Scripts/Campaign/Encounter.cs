using UnityEngine.SceneManagement;

public class Encounter
{
    public Party Initiator;
    public Party Target;

    public void Attack()
    {
        SceneManager.LoadScene("NavalTestArena");
    }
}