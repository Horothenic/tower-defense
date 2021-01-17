using UnityEngine;
using System.Collections;

namespace TowerDefense.Towers
{
    [RequireComponent(typeof(TowerEnemyDetector))]
    public class MegaTowerShooter : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private LaserDamager megaLaser = null;
        [SerializeField] private Collider megaLaserCollider = null;
        [SerializeField] private float duration = 5;
        [SerializeField] private float interval = 0.5f;
        [SerializeField] private float damage = 10;
        [SerializeField] private float cooldown = 10;

        private TowerEnemyDetector towerEnemyDetector = null;
        private bool shotEnable = false;
        private Coroutine shotCoroutine = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            towerEnemyDetector = GetComponent<TowerEnemyDetector>();
        }

        private void Start()
        {
            megaLaser.SetDamage(damage);
            Invoke(nameof(EnableShot), cooldown);
        }

        private void Update()
        {
            if (shotCoroutine == null || towerEnemyDetector.HasEnemies)
                return;

            StopCoroutine(shotCoroutine);
            Stop();
        }

        public void Shoot()
        {
            if (!shotEnable)
                return;

            shotEnable = false;
            shotCoroutine = StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            megaLaser.gameObject.SetActive(true);

            var attacks = duration / interval;
            var wait = new WaitForSeconds(interval);

            for (int i = 0; i < attacks; i++)
            {
                megaLaserCollider.enabled = true;
                yield return wait;
                megaLaserCollider.enabled = false;
                yield return null;
            }

            Stop();
        }

        private void Stop()
        {
            megaLaser.gameObject.SetActive(false);
            Invoke(nameof(EnableShot), cooldown);
        }

        private void EnableShot()
        {
            shotEnable = true;
        }

        #endregion
    }
}
