using Assets.Practice.Lobby.Scripts.Inputs;
using Cinemachine;
using Mirror;
using UnityEngine;

namespace Assets.Practice.Lobby.Scripts
{
    public class PlayerCameraController : NetworkBehaviour
    {
        [Header("Camera")]
        [SerializeField] private readonly Vector2 _maxFollowOffset = new Vector2(-1f, 6f);

        [SerializeField] private readonly Vector2 _cameraVelocity = new Vector2(4f, 0.25f);
        [SerializeField] private readonly Transform _playerTransform = null;
        [SerializeField] private readonly CinemachineVirtualCamera _virtualCamera = null;

        private Controls _controls;

        private Controls Controls
        {
            get
            {
                if (_controls != null) { return _controls; }
                return _controls = new Controls();
            }
        }

        private CinemachineTransposer _transposer;

        public override void OnStartAuthority()
        {
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

            _virtualCamera.gameObject.SetActive(true);

            enabled = true;

            Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        }

        [ClientCallback]
        private void OnEnable() => Controls.Enable();

        [ClientCallback]
        private void OnDisable() => Controls.Disable();

        private void Look(Vector2 lookAxis)
        {
            float deltaTime = Time.deltaTime;

            _transposer.m_FollowOffset.y = Mathf.Clamp(
                _transposer.m_FollowOffset.y - (lookAxis.y * _cameraVelocity.y * deltaTime),
                _maxFollowOffset.x,
                _maxFollowOffset.y);

            _playerTransform.Rotate(0f, lookAxis.x * _cameraVelocity.x * deltaTime, 0f);
        }
    }
}