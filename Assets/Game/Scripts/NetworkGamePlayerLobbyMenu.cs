using System;
using Mirror;

namespace Game.Scripts
{
    public class NetworkGamePlayerLobbyMenu : NetworkBehaviour
    {
        [SyncVar]
        private string _displayName = "Loading...";

        private NetworkManagerLobby _room;

        private NetworkManagerLobby Room
        {
            get
            {
                if (_room != null) { return _room; }
                return _room = NetworkManager.singleton as NetworkManagerLobby;
            }
        }

        public override void OnStartClient()
        {
            DontDestroyOnLoad(gameObject);

            Room.GamePlayers.Add(this);
        }

        [Obsolete("Override OnStopClient() instead")]
        public override void OnNetworkDestroy()
        {
            Room.GamePlayers.Remove(this);
        }

        [Server]
        public void SetDisplayName(string displayName)
        {
            _displayName = displayName;
        }
    }
}
