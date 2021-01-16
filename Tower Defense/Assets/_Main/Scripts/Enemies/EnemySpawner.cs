using UnityEngine;
using System.Collections;

using Utilities.Zenject;

namespace TowerDefense.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Transform path = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private EnemyAgent enemyPrefab = null;
        [SerializeField] private float enemySpawnDelay = 0.5f;

        #endregion

        #region BEHAVIORS

        public void SpawnWave(int enemies)
        {
            StartCoroutine(SpawnWaveCoroutine(enemies));
        }

        private IEnumerator SpawnWaveCoroutine(int enemies)
        {
            for (int i = 0; i < enemies; i++)
            {
                yield return new WaitForSeconds(enemySpawnDelay);
                SpawnEnemy(path);
            }
        }

        private void SpawnEnemy(Transform path)
        {
            var enemy = ZenjectUtilities.Instantiate(enemyPrefab, path.position, Quaternion.identity, path);
            enemy.LoadWaypoints(path);
        }

        #endregion
    }
}
