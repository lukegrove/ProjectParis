using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class MainMenu : MonoBehaviour
{
    public string NavalTestArena = "NavalTestArena";
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
        SceneManager.LoadSceneAsync(NavalTestArena);
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