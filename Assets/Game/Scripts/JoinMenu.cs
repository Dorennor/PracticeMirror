using UnityEngine;

namespace Game.Scripts
{
    public class JoinMenu : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject menuPage = null;
        [SerializeField] private GameObject joinPage = null;

        public void HostLobby()
        {

        }

        public void JoinLobby()
        {

        }

        public void Cancel()
        {
            menuPage.SetActive(true);
            joinPage.SetActive(false);
        }
    }
}
