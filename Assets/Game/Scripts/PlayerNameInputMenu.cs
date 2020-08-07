using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class PlayerNameInputMenu : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject loginPage = null;
        [SerializeField] private GameObject menuPage = null;
        [SerializeField] private Button submitButton = null;
        [SerializeField] private TMP_InputField nameInputField = null;

        private static string DisplayName { get; set; }

        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) return;

            var defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;
            submitButton.interactable = !string.IsNullOrEmpty(defaultName);
        }

        public void SavePlayerName()
        {
            DisplayName = nameInputField.text;

            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
            loginPage.SetActive(false);
            menuPage.SetActive(true);
        }
    }
}