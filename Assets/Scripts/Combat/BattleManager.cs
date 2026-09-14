using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public string CampaignMap = "CampaignMap";

    public void EndBattle(BattleResult result)
    {
        SceneManager.LoadScene(CampaignMap);
    }
}