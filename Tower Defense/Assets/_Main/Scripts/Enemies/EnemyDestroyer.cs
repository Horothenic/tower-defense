using UnityEngine;

using Zenject;
using Utilities.Events;

namespace TowerDefense.Enemies
{
    public class EnemyDestroyer : MonoBehaviour
    {
        #region FIELDS

        [Inject] private WavesManager wavesManager = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string destructionTag = null;

        #endregion

        #region EVENTS

        [HideInInspector] public UnityGameObjectEvent onDestroy = null;

        #endregion

        #region BEHAVIORS

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(destructionTag))
                return;

            DestroyEnemy();
        }

        public void DestroyEnemy()
        {
            wavesManager.EnemyDestroyed();
            onDestroy?.Invoke(gameObject);
            Destroy(gameObject);
        }

        #endregion
    }
}
