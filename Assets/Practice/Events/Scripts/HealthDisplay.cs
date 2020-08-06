using UnityEngine;
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