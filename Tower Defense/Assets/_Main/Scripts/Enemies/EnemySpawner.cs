using UnityEngine;
using System.Collections;

using Zenject;

namespace TowerDefense.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        #region FIELDS

        [Inject] private EnemyPool enemyPool = null;

        [Header("COMPONENTS")]
        [SerializeField] private Transform path = null;

        #endregion

        #region BEHAVIORS

        public void SpawnWave(EnemyAgent enemyPrefab, int enemies, float enemySpawnDelay)
        {
            StartCoroutine(SpawnWaveCoroutine(enemyPrefab, enemies, enemySpawnDelay));
        }

        private IEnumerator SpawnWaveCoroutine(EnemyAgent enemyPrefab, int enemies, float enemySpawnDelay)
        {
            for (int i = 0; i < enemies; i++)
            {
                yield return new WaitForSeconds(enemySpawnDelay);
                SpawnEnemy(enemyPrefab, path);
            }
        }

        private void SpawnEnemy(EnemyAgent enemyPrefab, Transform path)
        {
            var enemy = enemyPool.Spawn(enemyPrefab, path.position, Quaternion.identity);
            enemy.LoadWaypoints(path);
        }

        #endregion
    }
}
