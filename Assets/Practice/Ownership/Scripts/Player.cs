using Mirror;
using UnityEngine;

namespace Assets.Practice.Ownership.Scripts
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private readonly Vector3 _movement = new Vector3();

        [Client]
        private void Update()
        {
            if (!hasAuthority) { return; }

            if (!Input.GetKeyDown(KeyCode.Space)) { return; }

            //transform.Translate(movement);

            CmdMove();
        }

        [Command]
        private void CmdMove()
        {
            // Validate logic here

            RpcMove();
        }

        [ClientRpc]
        private void RpcMove() => transform.Translate(_movement);
    }
}