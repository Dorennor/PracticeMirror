using Mirror;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Practice.Chat.Scripts
{
    public class ChatBehaviour : NetworkBehaviour
    {
        [SerializeField] private readonly GameObject _chatUi = null;
        [SerializeField] private readonly TMP_Text _chatText = null;
        [SerializeField] private readonly TMP_InputField _inputField = null;

        private static event Action<string> OnMessage;

        public override void OnStartAuthority()
        {
            _chatUi.SetActive(true);

            OnMessage += HandleNewMessage;
        }

        [ClientCallback]
        private void OnDestroy()
        {
            if (!hasAuthority) { return; }

            OnMessage -= HandleNewMessage;
        }

        private void HandleNewMessage(string message)
        {
            _chatText.text += message;
        }

        [Client]
        public void Send(string message)
        {
            if (!Input.GetKeyDown(KeyCode.Return)) { return; }

            if (string.IsNullOrWhiteSpace(message)) { return; }

            CmdSendMessage(message);

            _inputField.text = string.Empty;
        }

        [Command]
        private void CmdSendMessage(string message)
        {
            RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
        }

        [ClientRpc]
        private void RpcHandleMessage(string message)
        {
            OnMessage?.Invoke($"\n{message}");
        }
    }
}