<<<<<<< HEAD:Assets/Game/Scripts/JoinMenu.cs
﻿using UnityEngine;

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
=======
﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Practice.Events.Scripts
{
    public class HealthDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private readonly Health _health = null;

        [SerializeField] private readonly Image _healthBarImage = null;

        private void OnEnable()
        {
            _health.EventHealthChanged += HandleHealthChanged;
        }

        private void OnDisable()
        {
            _health.EventHealthChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(int currentHealth, int maxHealth)
        {
            _healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
>>>>>>> parent of 0688296... temp:Assets/Practice/Events/Scripts/HealthDisplay.cs
