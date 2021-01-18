using UnityEngine;
using System;

namespace TowerDefense.Enemies
{
    [Serializable]
    public struct WaveConfiguration
    {
        #region FIELDS

        [SerializeField] private int startWave;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private int numberOfRounds;
        [SerializeField] private float spawnDelay;
        [SerializeField] private EnemyAgent[] enemies;

        #endregion

        #region PROPERTIES

        public int StartWave => startWave;
        public int NumberOfEnemies => numberOfEnemies;
        public int NumberOfRounds => numberOfRounds;
        public float SpawnDelay => spawnDelay;
        public EnemyAgent[] Enemies => enemies;

        #endregion

        #region CONSTRUCTORS

        public WaveConfiguration(int startWave, int numberOfEnemies, int numberOfRounds, float spawnDelay, EnemyAgent[] enemies)
        {
            this.startWave = startWave;
            this.numberOfEnemies = numberOfEnemies;
            this.numberOfRounds = numberOfRounds;
            this.spawnDelay = spawnDelay;
            this.enemies = enemies;
        }

        #endregion

        #region BEHAVIORS

        public EnemyAgent GetEnemy(int index)
        {
            index = Mathf.Clamp(index, 0, enemies.Length - 1);
            return enemies[index];
        }

        #endregion
    }
}
