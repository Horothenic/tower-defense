using UnityEngine;

using TowerDefense.Enemies;

namespace TowerDefense.Towers
{
    public class LaserDamager : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private string enemyTag = "Enemy";
        [SerializeField] private bool destroyOnContact = true;

        private float damage = 1;

        #endregion

        #region BEHAVIORS

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(enemyTag))
                return;

            DamageEnemy(other.gameObject.GetComponent<EnemyHealth>());

            if (destroyOnContact)
                DestroyLaser();
        }

        public void SetDamage(float damage)
        {
            this.damage = damage;
        }

        private void DamageEnemy(EnemyHealth enemyHealth)
        {
            if (enemyHealth == null)
                return;

            enemyHealth.DecreaseHealth(damage);
        }

        private void DestroyLaser()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
