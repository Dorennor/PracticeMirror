using System;
using Mirror;
using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class Chat : NetworkBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject chatUi = null;

        [SerializeField] private TMP_Text chatText = null;
        [SerializeField] private TMP_InputField inputField = null;

        private static event Action<string> OnMessage;

        public override void OnStartAuthority()
        {
            OnMessage += HandleNewMessage;
            Debug.Log("OnStartAuthority");
        }

        [ClientCallback]
        private void OnDestroy()
        {
            if (!hasAuthority) { return; }

            OnMessage -= HandleNewMessage;
            Debug.Log("OnDestroy");
        }

        private void HandleNewMessage(string message)
        {
            chatText.text += message;
            Debug.Log("HandleNewMessage");
        }

        [Client]
        public void Send(string message)
        {
            if (!Input.GetKeyDown(KeyCode.Return)) { return; }

            if (string.IsNullOrWhiteSpace(message)) { return; }
            Debug.Log("Send");

            CmdSendMessage(message);

            inputField.text = string.Empty;
        }

        [Command]
        private void CmdSendMessage(string message)
        {
            Debug.Log("CmdSendMessage");
            RpcHandleMessage($"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} | [{connectionToClient.connectionId}]: {message}");
        }

        [ClientRpc]
        private void RpcHandleMessage(string message)
        {
            Debug.Log("RpcHandleMessage");
            OnMessage?.Invoke($"\n{message}");
        }
    }
}