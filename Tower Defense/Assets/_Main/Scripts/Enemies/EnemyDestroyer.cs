using UnityEngine;

using Zenject;

namespace TowerDefense.Enemies
{
    public class EnemyDestroyer : MonoBehaviour
    {
        #region FIELDS

        [Inject] private WavesManager wavesManager = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string destructionTag = null;

        #endregion

        #region BEHAVIORS

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(destructionTag))
                return;

            wavesManager.EnemyDestroyed();
            DestroyEnemy();
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
