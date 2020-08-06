using System;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class NetworkRoomPlayerLobby : NetworkBehaviour
    {
        [Header("UI")]
        [SerializeField] private readonly GameObject _lobbyUi = null;

        [SerializeField] private readonly TMP_Text[] _playerNameTexts = new TMP_Text[4];
        [SerializeField] private readonly TMP_Text[] _playerReadyTexts = new TMP_Text[4];
        [SerializeField] private readonly Button _startGameButton = null;

        [SyncVar(hook = nameof(HandleDisplayNameChanged))]
        public string displayName = "Loading...";

        [SyncVar(hook = nameof(HandleReadyStatusChanged))]
        public bool isReady;

        private bool _isLeader;

        public bool IsLeader
        {
            set
            {
                _isLeader = value;
                _startGameButton.gameObject.SetActive(value);
            }
        }

        private NetworkManagerLobby _room;

        private NetworkManagerLobby Room
        {
            get
            {
                if (_room != null) { return _room; }
                return _room = NetworkManager.singleton as NetworkManagerLobby;
            }
        }

        public override void OnStartAuthority()
        {
            CmdSetDisplayName(PlayerNameInput.DisplayName);

            _lobbyUi.SetActive(true);
        }

        public override void OnStartClient()
        {
            Room.RoomPlayers.Add(this);

            UpdateDisplay();
        }

        [Obsolete("Override OnStopClient() instead")]
        public override void OnNetworkDestroy()
        {
            Room.RoomPlayers.Remove(this);

            UpdateDisplay();
        }

        public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();

        public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

        private void UpdateDisplay()
        {
            if (!hasAuthority)
            {
                foreach (var player in Room.RoomPlayers.Where(player => player.hasAuthority))
                {
                    player.UpdateDisplay();
                    break;
                }

                return;
            }

            for (int i = 0; i < _playerNameTexts.Length; i++)
            {
                _playerNameTexts[i].text = "Waiting For Player...";
                _playerReadyTexts[i].text = string.Empty;
            }

            for (int i = 0; i < Room.RoomPlayers.Count; i++)
            {
                _playerNameTexts[i].text = Room.RoomPlayers[i].displayName;
                _playerReadyTexts[i].text = Room.RoomPlayers[i].isReady ?
                    "<color=green>Ready</color>" :
                    "<color=red>Not Ready</color>";
            }
        }

        public void HandleReadyToStart(bool readyToStart)
        {
            if (!_isLeader) { return; }

            _startGameButton.interactable = readyToStart;
        }

        [Command]
        private void CmdSetDisplayName(string displayName)
        {
            this.displayName = displayName;
        }

        [Command]
        public void CmdReadyUp()
        {
            isReady = !isReady;

            Room.NotifyPlayersOfReadyState();
        }

        [Command]
        public void CmdStartGame()
        {
            if (Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }

            Room.StartGame();
        }
    }
}
