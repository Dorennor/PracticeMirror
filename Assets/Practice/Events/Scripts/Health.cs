using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Practice.Events.Scripts
{
    public class Health : NetworkBehaviour
    {
        [Header("Settings")]
        [SerializeField] private readonly int _maxHealth = 100;

        [SerializeField] private readonly int _damagePerPress = 10;

        [SyncVar]
        private int _currentHealth;

        public delegate void HealthChangedDelegate(int currentHealth, int maxHealth);

        [SyncEvent]
        public event HealthChangedDelegate EventHealthChanged;

        #region Server

        private void SetHealth(int value)
        {
            _currentHealth = value;
            EventHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public override void OnStartServer() => SetHealth(_maxHealth);

        [Command]
        private void CmdDealDamage() => SetHealth(Mathf.Max(_currentHealth - _damagePerPress, 0));

        #endregion Server

        #region Client

        [ClientCallback]
        private void Update()
        {
            if (!hasAuthority) { return; }

            if (!Keyboard.current.spaceKey.wasPressedThisFrame) { return; }

            CmdDealDamage();
        }

        #endregion Client
    }
}