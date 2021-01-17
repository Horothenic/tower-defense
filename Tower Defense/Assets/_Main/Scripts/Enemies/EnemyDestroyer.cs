using UnityEngine;

using Zenject;
using Utilities.Events;

using TowerDefense.Particles;

namespace TowerDefense.Enemies
{
    public class EnemyDestroyer : MonoBehaviour
    {
        #region FIELDS

        [Inject] private WavesManager wavesManager = null;
        [Inject] private ParticlesPool particlesPool = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string destructionTag = null;

        [Header("PARTICLES")]
        [SerializeField] private GameObject explosionParticles = null;

        #endregion

        #region EVENTS

        [Header("EVENTS")]
        public UnityGameObjectEvent onDestroy = null;

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
            particlesPool.Spawn(explosionParticles, transform.position, Quaternion.identity);
            wavesManager.EnemyDestroyed();
            onDestroy?.Invoke(gameObject);
            gameObject.SetActive(false);
        }

        #endregion
    }
}
