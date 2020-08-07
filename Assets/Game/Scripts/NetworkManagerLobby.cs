using Mirror;
using UnityEngine;

namespace Game.Scripts
{
    public class NetworkManagerLobby : NetworkBehaviour
    {
        [SerializeField] private int minPlayers = 2;

        [Header("Maps")]
        [SerializeField] private int numberOfRounds = 1;
        //[SerializeField] private MapSet mapSet = null;

<<<<<<< HEAD
=======
        [SerializeField] private MapSet mapSet = null;

        [Header("Room")] [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

        [Header("Game")] [SerializeField] private NetworkGamePlayerLobby gamePlayerPrefab = null;

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

            if (SceneManager.GetActiveScene().name == menuScene) return;
            conn.Disconnect();
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            if (SceneManager.GetActiveScene().name != menuScene) return;
            var isLeader = RoomPlayers.Count == 0;

            var roomPlayerInstance = Instantiate(roomPlayerPrefab);

            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
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
            if (SceneManager.GetActiveScene().name != menuScene) return;
            if (!IsReadyToStart())
            {
                return;
            }

            _mapHandler = new MapHandler(mapSet, NumberOfRounds);

            ServerChangeScene(_mapHandler.NextMap);
        }

        public override void ServerChangeScene(string newSceneName)
        {
            if (SceneManager.GetActiveScene().name == menuScene && newSceneName.StartsWith("Scene_Map"))
            {
                for (var i = RoomPlayers.Count - 1; i >= 0; i--)
                {
                    var conn = RoomPlayers[i].connectionToClient;
                    var gameplayerInstance = Instantiate(gamePlayerPrefab);
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
>>>>>>> parent of 0688296... temp
    }
}
