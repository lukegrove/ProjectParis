using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ProjectParis.UI
{
    public sealed class MainMenu : MonoBehaviour
    {
        public string NavalTestArena = "NavalTestArena";
        public string CampaignMap = "CampaignMap";
        [SerializeField] private GameObject firstSelectedButton;

        private void Start()
        {
            /*if (firstSelectedButton != null)
            {
                EventSystem.current.SetSelectedGameObject(firstSelectedButton);
            }*/
        }

        public void Play()
        {
            SceneManager.LoadSceneAsync(CampaignMap);
        }

        public void Quit()
        {
            //#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            /*#else
            Application.Quit();
            #endif*/
        }
    }
}