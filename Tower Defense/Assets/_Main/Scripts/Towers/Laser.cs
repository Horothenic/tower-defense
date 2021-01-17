using UnityEngine;

using TowerDefense.Enemies;

namespace TowerDefense.Towers
{
    public class Laser : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Transform rotationMaster = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string enemyTag = "Enemy";
        [SerializeField] private float speed = 10;
        [SerializeField] private float rotationSpeed = 1;

        private float damage = 1;
        private GameObject target = null;

        #endregion

        #region BEHAVIORS

        private void FixedUpdate()
        {
            if (target == null)
            {
                DestroyLaser();
                return;
            }

            Move();
            Rotate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(enemyTag))
                return;

            DamageEnemy(other.gameObject.GetComponent<EnemyHealth>());
            DestroyLaser();
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }

        public void SetDamage(float damage)
        {
            this.damage = damage;
        }

        private void Move()
        {
            var step = Time.fixedDeltaTime * speed;
            var newPosition = Vector3.MoveTowards(transform.position, target.transform.position, step);
            transform.position = newPosition;
        }

        private void Rotate()
        {
            rotationMaster.LookAt(target.transform, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationMaster.rotation, rotationSpeed * Time.deltaTime);
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
