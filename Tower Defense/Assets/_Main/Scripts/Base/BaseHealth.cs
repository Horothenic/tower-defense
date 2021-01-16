using UnityEngine;
using UnityEngine.Events;

using Utilities.Inspector;

namespace TowerDefense.Base
{
    public class BaseHealth : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATION")]
        [SerializeField] private string enemyTag = "Enemy";
        [SerializeField] private float health = 50;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private float currentHealth = 0;

        #endregion

        #region EVENTS

        [Header("EVENTS")]
        public UnityEvent onHealthDepleted = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            currentHealth = health;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(enemyTag))
                return;

            ReceiveHit();
        }

        public void ReceiveHit()
        {
            if (--currentHealth <= 0)
                onHealthDepleted?.Invoke();
        }

        #endregion
    }
}
