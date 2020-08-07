using System.Collections.Generic;
using System.Linq;
using Assets.Practice.Lobby.Scripts;
using UnityEngine;

namespace Practice.Lobby.Scripts
{
    public class MapHandler
    {
        private readonly IReadOnlyCollection<string> _maps;
        private readonly int _numberOfRounds;

        private int _currentRound;
        private List<string> _remainingMaps;

        public MapHandler(MapSet mapSet, int numberOfRounds)
        {
            _maps = mapSet.Maps;
            _numberOfRounds = numberOfRounds;

            ResetMaps();
        }

        private bool IsComplete => _currentRound == _numberOfRounds;

        public string NextMap
        {
            get
            {
                if (IsComplete) { return null; }

                _currentRound++;

                if (_remainingMaps.Count == 0) { ResetMaps(); }

                var map = _remainingMaps[Random.Range(0, _remainingMaps.Count)];

                _remainingMaps.Remove(map);

                return map;
            }
        }

        private void ResetMaps() => _remainingMaps = _maps.ToList();
    }
}