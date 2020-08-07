using Assets.Practice.Lobby.Scripts.Inputs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Practice.Lobby.Scripts
{
    public class InputManager : MonoBehaviour
    {
        private static readonly IDictionary<string, int> MapStates = new Dictionary<string, int>();

        private static Controls _controls;

        public static Controls Controls
        {
            get
            {
                if (_controls != null) { return _controls; }
                return _controls = new Controls();
            }
        }

        private void Awake()
        {
            if (_controls != null) { return; }
            _controls = new Controls();
        }

        private void OnEnable() => Controls.Enable();

        private void OnDisable() => Controls.Disable();

        private void OnDestroy() => _controls = null;

        public static void Add(string mapName)
        {
            MapStates.TryGetValue(mapName, out int value);
            MapStates[mapName] = value + 1;

            UpdateMapState(mapName);
        }

        public static void Remove(string mapName)
        {
            MapStates.TryGetValue(mapName, out int value);
            MapStates[mapName] = Mathf.Max(value - 1, 0);

            UpdateMapState(mapName);
        }

        private static void UpdateMapState(string mapName)
        {
            int value = MapStates[mapName];

            if (value > 0)
            {
                Controls.Asset.FindActionMap(mapName).Disable();

                return;
            }

            Controls.Asset.FindActionMap(mapName).Enable();
        }
    }
}