using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Zenject;
using Utilities.Inspector;

namespace TowerDefense.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        #region FIELDS

        [Inject] private WavesManager wavesManager = null;

        [Header("COMPONENTS")]
        [SerializeField] private Image healthBar = null;

        [Header("CONFIGURATION")]
        [SerializeField] private float baseHealth = 1;
        [SerializeField] private float extraHealthModifier = 1;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private float realHealth = 1;
        [ReadOnly] [SerializeField] private float maxHealth = 1;

        #endregion

        #region EVENTS

        [Header("EVENTS")]
        public UnityEvent onHealthDepleted = null;

        #endregion

        #region BEHAVIORS

        private void OnEnable()
        {
            LoadHealth(wavesManager.Wave);
        }

        private void LoadHealth(int extraHealth)
        {
            maxHealth = realHealth = baseHealth + (extraHealth * extraHealthModifier);
            UpdateHealthBar();
        }

        public void DecreaseHealth(float damage)
        {
            realHealth -= damage;

            if (realHealth <= 0)
                onHealthDepleted?.Invoke();

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = Mathf.InverseLerp(0, maxHealth, realHealth);
        }

        #endregion
    }
}
