using UnityEngine;
using UnityEngine.Events;

using Zenject;
using Utilities.Inspector;

using TowerDefense.UI;

namespace TowerDefense.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        #region FIELDS

        [Inject] private WavesManager wavesManager = null;
        [Inject] private DamageDealtPool damageDealtPool = null;

        [Header("CONFIGURATION")]
        [SerializeField] private float baseHealth = 1;
        [SerializeField] private float extraHealthModifier = 1;
        [SerializeField] private DamageDealtUI damageDealtPrefab = null;

        [Header("STATES")]
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
            damageDealtPool.Spawn(damageDealtPrefab, transform.position, damageDealtPrefab.transform.rotation).LoadDamageDealt(damage);
            realHealth -= damage;

            if (realHealth <= 0)
                onHealthDepleted?.Invoke();
        }

        #endregion
    }
}
