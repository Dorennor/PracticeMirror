using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Practice.Lobby.Scripts
{
    [CreateAssetMenu(fileName = "New Map Set", menuName = "Rounds/Map Set")]
    public class MapSet : ScriptableObject
    {
        [Scene]
        [SerializeField] private readonly List<string> _maps = new List<string>();

        public IReadOnlyCollection<string> Maps => _maps.AsReadOnly();
    }
}