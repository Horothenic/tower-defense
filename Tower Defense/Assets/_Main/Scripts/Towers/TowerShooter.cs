using UnityEngine;

using Utilities.Zenject;

namespace TowerDefense.Towers
{
    [RequireComponent(typeof(TowerEnemyDetector))]
    public class TowerShooter : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform laserOrigin = null;
        [SerializeField] private Laser laserPrefab = null;
        [SerializeField] private float shootDelay = 1;
        [SerializeField] private float damage = 1;

        private TowerEnemyDetector towerEnemyDetector = null;
        private GameObject currentTarget = null;
        private float currentTime = float.MaxValue;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            towerEnemyDetector = GetComponent<TowerEnemyDetector>();
            towerEnemyDetector.onRetarget.AddListener(SetTarget);
        }

        private void Update()
        {
            if (currentTime < shootDelay)
                currentTime += Time.deltaTime;

            if (currentTarget == null)
                return;

            if (currentTime >= shootDelay && towerEnemyDetector.LockedOn)
                Shoot();
        }

        public void SetTarget(GameObject newTarget)
        {
            currentTarget = newTarget;
        }

        private void Shoot()
        {
            currentTime = 0;
            var laser = ZenjectUtilities.Instantiate(laserPrefab, laserOrigin.position, laserOrigin.rotation);
            laser.SetTarget(currentTarget);
            laser.SetDamage(damage);
        }

        #endregion
    }
}
