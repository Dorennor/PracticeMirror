using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class ConnectionMenu : MonoBehaviour
    {
        [SerializeField] private NetworkManagerLobby networkManagerLobby = null;

        [Header("UI")]
        [SerializeField] private GameObject landingPagePanel = null;

        [SerializeField] private TMP_InputField ipAddressInputField = null;
        [SerializeField] private Button submitButton = null;

        private void OnEnable()
        {
            NetworkManagerLobby.OnClientConnected += HandleClientConnected;
            NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
        }

        private void OnDisable()
        {
            NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
            NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
        }

        public void JoinLobby()
        {
            Debug.Log(ipAddressInputField.text);
            var ipAddress = ipAddressInputField.text;
            Debug.Log(ipAddress);
            networkManagerLobby.networkAddress = ipAddress;
            networkManagerLobby.StartClient();
            submitButton.interactable = false;
        }

        private void HandleClientConnected()
        {
            submitButton.interactable = true;

            gameObject.SetActive(false);
            landingPagePanel.SetActive(false);
        }

        private void HandleClientDisconnected()
        {
            submitButton.interactable = true;
        }
    }
}