using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Practice.Lobby.Scripts;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Practice.Lobby.Scripts
{
    public class NetworkManagerLobby : NetworkManager
    {
        [SerializeField] private const int MinPlayers = 2;
        [Scene] [SerializeField] private readonly string _menuScene = string.Empty;

        [Header("Maps")] [SerializeField] private const int NumberOfRounds = 1;

        [SerializeField] private readonly MapSet _mapSet = null;

        [Header("Room")]
        [SerializeField] private readonly NetworkRoomPlayerLobby _roomPlayerPrefab = null;

        [Header("Game")]
        [SerializeField] private readonly NetworkGamePlayerLobby _gamePlayerPrefab = null;

        //[SerializeField] private readonly GameObject _playerSpawnSystem = null;
        //[SerializeField] private readonly GameObject _roundSystem = null;

        private MapHandler _mapHandler;

        public static event Action OnClientConnected;

        public static event Action OnClientDisconnected;

        public static event Action<NetworkConnection> OnServerReadied;

        public static event Action OnServerStopped;

        public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();
        public List<NetworkGamePlayerLobby> GamePlayers { get; } = new List<NetworkGamePlayerLobby>();

        public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

        public override void OnStartClient()
        {
            var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

            foreach (var prefab in spawnablePrefabs)
            {
                ClientScene.RegisterPrefab(prefab);
            }
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            OnClientConnected?.Invoke();
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);

            OnClientDisconnected?.Invoke();
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
                return;
            }

            if (SceneManager.GetActiveScene().name == _menuScene) return;
            conn.Disconnect();
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            if (SceneManager.GetActiveScene().name == _menuScene)
            {
                bool isLeader = RoomPlayers.Count == 0;

                NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(_roomPlayerPrefab);

                roomPlayerInstance.IsLeader = isLeader;

                NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

                RoomPlayers.Remove(player);

                NotifyPlayersOfReadyState();
            }

            base.OnServerDisconnect(conn);
        }

        public override void OnStopServer()
        {
            OnServerStopped?.Invoke();

            RoomPlayers.Clear();
            GamePlayers.Clear();
        }

        public void NotifyPlayersOfReadyState()
        {
            foreach (var player in RoomPlayers)
            {
                player.HandleReadyToStart(IsReadyToStart());
            }
        }

        private bool IsReadyToStart()
        {
            return numPlayers >= MinPlayers && RoomPlayers.All(player => player.isReady);
        }

        public void StartGame()
        {
            if (SceneManager.GetActiveScene().name != _menuScene) return;
            if (!IsReadyToStart()) { return; }

            _mapHandler = new MapHandler(_mapSet, NumberOfRounds);

            ServerChangeScene(_mapHandler.NextMap);
        }

        public override void ServerChangeScene(string newSceneName)
        {
            // From menu to game
            if (SceneManager.GetActiveScene().name == _menuScene && newSceneName.StartsWith("Scene_Map"))
            {
                for (var i = RoomPlayers.Count - 1; i >= 0; i--)
                {
                    var conn = RoomPlayers[i].connectionToClient;
                    var gameplayerInstance = Instantiate(_gamePlayerPrefab);
                    gameplayerInstance.SetDisplayName(RoomPlayers[i].displayName);

                    NetworkServer.Destroy(conn.identity.gameObject);

                    NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
                }
            }

            base.ServerChangeScene(newSceneName);
        }

        //public override void OnServerSceneChanged(string sceneName)
        //{
        //    if (sceneName.StartsWith("Scene_Map"))
        //    {
        //        GameObject playerSpawnSystemInstance = Instantiate(_playerSpawnSystem);
        //        NetworkServer.Spawn(playerSpawnSystemInstance);

        //        GameObject roundSystemInstance = Instantiate(_roundSystem);
        //        NetworkServer.Spawn(roundSystemInstance);
        //    }
        //}

        public override void OnServerReady(NetworkConnection conn)
        {
            base.OnServerReady(conn);

            OnServerReadied?.Invoke(conn);
        }
    }
}