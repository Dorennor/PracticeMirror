using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class PlayerNameInput : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private readonly TMP_InputField _nameInputField = null;

        [SerializeField] private readonly Button _continueButton = null;

        public static string DisplayName { get; private set; }

        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

            var defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            _nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        private void SetPlayerName(string name)
        {
            _continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            DisplayName = _nameInputField.text;

            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }
    }
}
