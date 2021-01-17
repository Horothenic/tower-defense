using UnityEngine;

namespace TowerDefense.Towers
{
    public class LaserMovement : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Transform rotationMaster = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float speed = 10;
        [SerializeField] private float rotationSpeed = 1;

        private GameObject target = null;

        #endregion

        #region BEHAVIORS

        private void FixedUpdate()
        {
            if (target == null || !target.activeSelf)
            {
                DestroyLaser();
                return;
            }

            Move();
            Rotate();
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
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

        private void DestroyLaser()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
