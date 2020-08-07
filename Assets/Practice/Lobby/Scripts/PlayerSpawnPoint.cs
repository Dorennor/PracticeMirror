using UnityEngine;

namespace Practice.Lobby.Scripts
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        private void Awake() => PlayerSpawnSystem.AddSpawnPoint(transform);

        private void OnDestroy() => PlayerSpawnSystem.RemoveSpawnPoint(transform);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            var position = transform.position;
            Gizmos.DrawSphere(position, 1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position, position + transform.forward * 2);
        }
    }
}