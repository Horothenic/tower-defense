using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

using NUnit.Framework;

using TowerDefense.Towers;
using TowerDefense.Enemies;

namespace Tests
{
    public class EnemyDamageTests : Test
    {
        #region BEHAVIORS

        private const string EnemyDamageTestSceneName = "EnemyDamageTestScene";
        private const string ParentName = "Parent";

        private EnemyAgent enemy = null;
        private LaserMovement laser = null;

        #endregion

        #region BEHAVIORS

        [SetUp]
        public void SetUp()
        {
            LoadScene(EnemyDamageTestSceneName);
            SetFastTimescale();
        }

        [TearDown]
        public void TearDown()
        {
            UnloadScene(EnemyDamageTestSceneName);
            ResetTimescale();
        }

        [UnityTest]
        public IEnumerator LaserDontMoveUntilTarget()
        {
            yield return null;

            FindGameObjects();

            Assert.AreEqual(Vector3.zero, laser.transform.position);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(Vector3.zero, laser.transform.position);
        }

        [UnityTest]
        public IEnumerator LaserDestroysEnemy()
        {
            yield return null;

            FindGameObjects();

            laser.gameObject.SetActive(true);
            laser.SetTarget(enemy.gameObject);

            yield return new WaitForSeconds(2);

            Assert.AreEqual(false, enemy.gameObject.activeSelf);
        }

        private void FindGameObjects()
        {
            var parent = GameObject.Find(ParentName);
            laser = parent.GetComponentInChildren<LaserMovement>(true);
            enemy = parent.GetComponentInChildren<EnemyAgent>();
        }

        #endregion
    }
}
