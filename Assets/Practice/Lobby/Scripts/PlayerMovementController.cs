using Mirror;
using UnityEngine;

namespace Assets.Practice.Lobby.Scripts
{
    public class PlayerMovementController : NetworkBehaviour
    {
        [SerializeField] private readonly float _movementSpeed = 5f;
        [SerializeField] private readonly CharacterController _controller = null;

        private Vector2 _previousInput;

        public override void OnStartAuthority()
        {
            enabled = true;

            InputManager.Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
            InputManager.Controls.Player.Move.canceled += ctx => ResetMovement();
        }

        [ClientCallback]
        private void Update() => Move();

        [Client]
        private void SetMovement(Vector2 movement) => _previousInput = movement;

        [Client]
        private void ResetMovement() => _previousInput = Vector2.zero;

        [Client]
        private void Move()
        {
            Vector3 right = _controller.transform.right;
            Vector3 forward = _controller.transform.forward;
            right.y = 0f;
            forward.y = 0f;

            Vector3 movement = right.normalized * _previousInput.x + forward.normalized * _previousInput.y;

            _controller.Move(movement * _movementSpeed * Time.deltaTime);
        }
    }
}