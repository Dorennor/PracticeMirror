using UnityEngine;

namespace Game.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject loginPage = null;
        [SerializeField] private GameObject menuPage = null;
        [SerializeField] private GameObject joinPage = null;

        public void PlayMenu()
        {
            menuPage.SetActive(false);
            joinPage.SetActive(true);
        }

        public void ChangeName()
        {
            menuPage.SetActive(false);
            loginPage.SetActive(true);
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void EditSettings()
        {

        }
    }
}
