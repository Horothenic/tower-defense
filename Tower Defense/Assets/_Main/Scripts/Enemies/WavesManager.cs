using UnityEngine;
using System.Collections;

using Utilities.Inspector;
using Utilities.Events;

namespace TowerDefense.Enemies
{
    public class WavesManager : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private EnemySpawner[] spawners = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private WavesSetup wavesSetup = null;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private int wave = default(int);

        private int currentEnemies = default(int);

        #endregion

        #region EVENTS

        [Header("EVENTS")]
        public UnityIntEvent onWaveUpdated = null;

        #endregion

        #region PROPERTIES

        public int Wave => wave;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            SpawnWave();
        }

        private void SpawnWave()
        {
            StartCoroutine(SpawnWaveCoroutine());
        }

        private IEnumerator SpawnWaveCoroutine()
        {
            wave++;
            onWaveUpdated?.Invoke(wave);

            var waveConfiguration = wavesSetup.GetWaveConfiguration(wave);

            currentEnemies = waveConfiguration.NumberOfEnemies * waveConfiguration.NumberOfRounds;
            var waitBetweenRounds = new WaitForSeconds(waveConfiguration.NumberOfEnemies * waveConfiguration.SpawnDelay);

            for (int i = 0; i < waveConfiguration.NumberOfRounds; i++)
            {
                var enemy = waveConfiguration.GetEnemy(i);
                GetRandomSpawner().SpawnWave(enemy, waveConfiguration.NumberOfEnemies, waveConfiguration.SpawnDelay);
                yield return waitBetweenRounds;
            }
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
