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

    }
}
