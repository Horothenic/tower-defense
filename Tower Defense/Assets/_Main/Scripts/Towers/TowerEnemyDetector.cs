using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using Utilities.Events;

using TowerDefense.Enemies;

namespace TowerDefense.Towers
{
    public class TowerEnemyDetector : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private RetargetType retargetType = default(RetargetType);
        [SerializeField] private string enemyTag = "Enemy";
        [SerializeField] private Transform tower = null;
        [SerializeField] private Transform rotationMaster = null;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float lockedOnThreshold = 15f;

        private List<GameObject> enemiesDetected = new List<GameObject>();
        private GameObject currentTarget = null;

        #endregion

        #region EVENTS

        [HideInInspector] public UnityGameObjectEvent onRetarget = null;

        #endregion

        #region PROPERTIES

        public bool LockedOn { get; private set; } = false;

        #endregion

        #region BEHAVIORS

        private void Update()
        {
            if (currentTarget == null)
                return;

            rotationMaster.LookAt(currentTarget.transform, Vector3.up);
            tower.rotation = Quaternion.Lerp(tower.rotation, rotationMaster.rotation, rotationSpeed * Time.deltaTime);

            if (LockedOn)
                return;

            LockedOn = Quaternion.Angle(tower.rotation, rotationMaster.rotation) < lockedOnThreshold;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(enemyTag))
                return;

            enemiesDetected.Add(other.gameObject);
            other.gameObject.GetComponent<EnemyDestroyer>().onDestroy.AddListener(OnEnemyDestroy);

            if (currentTarget == null)
                Retarget();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(enemyTag))
                return;

            enemiesDetected.Remove(other.gameObject);
            other.gameObject.GetComponent<EnemyDestroyer>().onDestroy.RemoveListener(OnEnemyDestroy);

            if (currentTarget == other.gameObject)
                Retarget();
        }

        private void Retarget()
        {
            LockedOn = false;

            if (enemiesDetected.Count == 0)
            {
                currentTarget = null;
            }
            else
            {
                switch (retargetType)
                {
                    case RetargetType.FirstDetected:
                        currentTarget = enemiesDetected.First();
                        break;
                    case RetargetType.LastDetected:
                        currentTarget = enemiesDetected.Last();
                        break;
                }
            }

            onRetarget?.Invoke(currentTarget);
        }

        private void OnEnemyDestroy(GameObject enemy)
        {
            enemiesDetected.Remove(enemy);
            Retarget();
        }

        #endregion
    }
}
