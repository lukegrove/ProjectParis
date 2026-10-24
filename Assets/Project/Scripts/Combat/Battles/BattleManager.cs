using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectParis.Campaign;

namespace ProjectParis.Combat.Battles
{
    public class BattleManager : MonoBehaviour
    {
        public string CampaignMap = "CampaignMap";
        private Scene campaignScene;
        private Camera campaignCamera;
        private GameObject playerPartyObject;
        private GameObject campaignMapFloor;
        private bool campaignMapFloorWasActive;
        private CampaignManager campaignManager;

        private void Awake()
        {
            campaignScene = SceneManager.GetSceneByName(CampaignMap);
            if (!campaignScene.IsValid() || !campaignScene.isLoaded)
            {
                Debug.LogError($"Campaign scene '{CampaignMap}' is not loaded.");
                return;
            }

            foreach (GameObject root in campaignScene.GetRootGameObjects())
            {
                foreach (Camera sceneCamera in root.GetComponentsInChildren<Camera>(true))
                {
                    if (campaignCamera == null && sceneCamera.CompareTag("MainCamera"))
                    {
                        campaignCamera = sceneCamera;
                    }
                }

                if (campaignManager == null)
                {
                    campaignManager = root.GetComponentInChildren<CampaignManager>(true);
                }

                if (playerPartyObject == null)
                {
                    CampaignInputManager campaignInput = root.GetComponentInChildren<CampaignInputManager>(true);
                    if (campaignInput != null)
                    {
                        playerPartyObject = campaignInput.gameObject;
                    }
                }

                if (root.name == "CampaignMap")
                {
                    campaignMapFloor = root;
                    campaignMapFloorWasActive = root.activeSelf;
                }
            }

            if (campaignMapFloor != null)
            {
                campaignMapFloor.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No CampaignMap floor found in CampaignMap.");
            }

            if (campaignCamera != null)
            {
                campaignCamera.enabled = false;
            }
            else
            {
                Debug.LogWarning("No MainCamera found in CampaignMap.");
            }

            if (playerPartyObject != null)
            {
                playerPartyObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No player party with CampaignInputManager found in CampaignMap.");
            }
        }

        public void EndBattle(BattleResult battleResult)
        {
            if (!campaignScene.IsValid() || !campaignScene.isLoaded || !SceneManager.SetActiveScene(campaignScene))
            {
                Debug.LogError($"Could not make campaign scene '{CampaignMap}' active; Battle was not unloaded.");
                return;
            }

            if (campaignMapFloor != null)
            {
                campaignMapFloor.SetActive(campaignMapFloorWasActive);
            }

            if (playerPartyObject != null)
            {
                playerPartyObject.SetActive(true);
            }

            if (campaignCamera != null)
            {
                campaignCamera.enabled = true;
            }

            if (campaignManager != null)
            {
                campaignManager.EndBattle(battleResult);
            }
            else
            {
                Debug.LogWarning("No CampaignManager found; unloading Battle without applying campaign results.");
            }

            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}