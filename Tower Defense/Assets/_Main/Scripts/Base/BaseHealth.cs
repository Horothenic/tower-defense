using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Zenject;
using Utilities.Inspector;
using Utilities.Cameras;

namespace TowerDefense.Base
{
    public class BaseHealth : MonoBehaviour
    {
        #region FIELDS

        [Inject] private CameraShake cameraShake = null;

        [Header("COMPONENTS")]
        [SerializeField] private Image healthBar = null;

        [Header("CONFIGURATION")]
        [SerializeField] private string enemyTag = "Enemy";
        [SerializeField] private float health = 50;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private float currentHealth = 0;
        [ReadOnly] [SerializeField] private float maxHealth = 1;

        private bool destroyed = false;

        #endregion

        #region EVENTS

        [Header("EVENTS")]
        public UnityEvent onHealthDepleted = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            maxHealth = currentHealth = health;
            UpdateHealthBar();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(enemyTag))
                return;

            if (destroyed)
                return;

            ReceiveHit();
        }

        public void ReceiveHit()
        {
            cameraShake.SmallShake();

            if (--currentHealth <= 0)
            {
                destroyed = true;
                onHealthDepleted?.Invoke();
            }

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = Mathf.InverseLerp(0, maxHealth, currentHealth);
        }

        #endregion
    }
}
