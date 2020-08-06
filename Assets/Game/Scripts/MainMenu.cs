using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject mainMenuPanel;

        [SerializeField] private GameObject landingPagePanel;

        public void QuitGame()
        {
            #if UNITY_EDITOR

                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}