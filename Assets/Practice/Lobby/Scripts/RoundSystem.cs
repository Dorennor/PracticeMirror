using System.Linq;
using Assets.Practice.Lobby.Scripts;
using Mirror;
using UnityEngine;

namespace Practice.Lobby.Scripts
{
    public class RoundSystem : NetworkBehaviour
    {
        [SerializeField] private readonly Animator _animator = null;

        private NetworkManagerLobby _room;

        private NetworkManagerLobby Room
        {
            get
            {
                if (_room != null) { return _room; }
                return _room = NetworkManager.singleton as NetworkManagerLobby;
            }
        }

        public void CountdownEnded()
        {
            _animator.enabled = false;
        }

        #region Server

        public override void OnStartServer()
        {
            NetworkManagerLobby.OnServerStopped += CleanUpServer;
            NetworkManagerLobby.OnServerReadied += CheckToStartRound;
        }

        [ServerCallback]
        private void OnDestroy() => CleanUpServer();

        [Server]
        private void CleanUpServer()
        {
            NetworkManagerLobby.OnServerStopped -= CleanUpServer;
            NetworkManagerLobby.OnServerReadied -= CheckToStartRound;
        }

        [ServerCallback]
        public void StartRound()
        {
            RpcStartRound();
        }

        [Server]
        private void CheckToStartRound(NetworkConnection conn)
        {
            if (Room.GamePlayers.Count(x => x.connectionToClient.isReady) != Room.GamePlayers.Count) { return; }

            _animator.enabled = true;

            RpcStartCountdown();
        }

        #endregion Server

        #region Client

        [ClientRpc]
        private void RpcStartCountdown()
        {
            _animator.enabled = true;
        }

        [ClientRpc]
        private void RpcStartRound()
        {
            InputManager.Remove(ActionMapNames.Player);
        }

        #endregion Client
    }
}