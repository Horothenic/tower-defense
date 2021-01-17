using UnityEngine;

using Zenject;

using Utilities.Zenject;
using Utilities.Inspector;

namespace TowerDefense.Towers
{
    [RequireComponent(typeof(TowerEnemyDetector))]
    public class TowerShooter : MonoBehaviour
    {
        #region FIELDS

        [Inject] private LaserPool laserPool = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform laserOrigin = null;
        [SerializeField] private LaserMovement laserPrefab = null;
        [SerializeField] private float shootDelay = 1;
        [SerializeField] private float damage = 1;
        [SerializeField] private float damageIncrementPerLevel = 1;
        [SerializeField] private float maxLevel = 10;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private float realDamage = 0;
        [ReadOnly] [SerializeField] private int level = 1;

        private TowerEnemyDetector towerEnemyDetector = null;
        private GameObject currentTarget = null;
        private float currentTime = float.MaxValue;

        #endregion

        #region PROPERTIES

        public bool IsMaxLevel => level >= maxLevel;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            towerEnemyDetector = GetComponent<TowerEnemyDetector>();
            towerEnemyDetector.onRetarget.AddListener(SetTarget);
            CalculateDamage();
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
            var laser = laserPool.Spawn(laserPrefab, laserOrigin.position, laserOrigin.rotation);
            laser.SetTarget(currentTarget);

            var laserDamager = laser.GetComponent<LaserDamager>();
            laserDamager.SetDamage(realDamage);
        }

        private void CalculateDamage()
        {
            realDamage = damage + (damageIncrementPerLevel * level);
        }

        public void IncreaseLevel()
        {
            if (IsMaxLevel)
                return;

            level++;
            CalculateDamage();
        }

        #endregion
    }
}
