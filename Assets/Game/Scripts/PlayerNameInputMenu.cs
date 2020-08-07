<<<<<<< HEAD:Assets/Game/Scripts/PlayerNameInputMenu.cs
﻿using TMPro;
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
=======
﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Practice.Lobby.Scripts
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

        public void SetPlayerName(string name)
        {
            _continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            DisplayName = _nameInputField.text;

            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }
    }
>>>>>>> parent of 0688296... temp:Assets/Practice/Lobby/Scripts/PlayerNameInput.cs
}