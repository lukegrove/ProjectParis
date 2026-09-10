using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public string MainMenu = "MainMenu";

    public void EndBattle(BattleResult result)
    {
        // Store/pass result
        SceneManager.LoadScene(MainMenu);
    }
}