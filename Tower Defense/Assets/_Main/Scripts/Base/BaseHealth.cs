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

        private bool destroyed = false;

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

            if (destroyed)
                return;

            ReceiveHit();
        }

        public void ReceiveHit()
        {
            if (--currentHealth <= 0)
            {
                destroyed = true;
                onHealthDepleted?.Invoke();
            }
        }

        #endregion
    }
}
