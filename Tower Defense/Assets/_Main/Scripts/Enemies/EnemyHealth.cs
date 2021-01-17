using UnityEngine;
using UnityEngine.Events;

using Zenject;
using Utilities.Inspector;

namespace TowerDefense.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        #region FIELDS

        [Inject] private WavesManager wavesManager = null;

        [Header("CONFIGURATION")]
        [SerializeField] private float baseHealth = 1;
        [SerializeField] private float extraHealthModifier = 1;
        [ReadOnly] [SerializeField] private float realHealth = 1;

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
            realHealth = baseHealth + (extraHealth * extraHealthModifier);
        }

        public void DecreaseHealth(float damage)
        {
            realHealth -= damage;

            if (realHealth <= 0)
                onHealthDepleted?.Invoke();
        }

        #endregion
    }
}
