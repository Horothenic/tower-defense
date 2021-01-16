using UnityEngine;

using Utilities.Inspector;

namespace TowerDefense.Enemies
{
    public class WavesManager : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private EnemySpawner[] spawners = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int enemiesPerWave = 5;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private int wave = default(int);

        private int currentEnemies = default(int);

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            SpawnWave();
        }

        private void SpawnWave()
        {
            wave++;
            currentEnemies = enemiesPerWave;
            GetRandomSpawner().SpawnWave(enemiesPerWave);
        }

        private EnemySpawner GetRandomSpawner()
        {
            return spawners[Random.Range(0, spawners.Length)];
        }

        public void EnemyDestroyed()
        {
            if (--currentEnemies == 0)
                SpawnWave();
        }

        #endregion
    }
}
