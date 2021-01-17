using UnityEngine;
using System.Collections.Generic;

namespace TowerDefense.Enemies
{
    public class EnemyAgent : MonoBehaviour
    {
        #region FIELDS

        private const float DistanceToRetarget = 0.1f;

        [Header("COMPONENTS")]
        [SerializeField] private new Rigidbody rigidbody = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float speed = 1;

        private List<Transform> waypoints = new List<Transform>();
        private int currentWaypointIndex = default(int);
        private Transform currentWaypoint = null;

        #endregion

        #region BEHAVIORS

        private void OnEnable()
        {
            currentWaypointIndex = 0;
        }

        private void FixedUpdate()
        {
            if (currentWaypoint == null)
                return;

            var waypointPosition = currentWaypoint.position;
            var step = Time.fixedDeltaTime * speed;
            var newPosition = Vector3.MoveTowards(rigidbody.position, waypointPosition, step);
            rigidbody.MovePosition(newPosition);

            if (Vector3.Distance(rigidbody.position, waypointPosition) < DistanceToRetarget)
                GetNextWaypoint();
        }

        public void LoadWaypoints(Transform path)
        {
            waypoints.Clear();

            foreach (Transform t in path)
                if (t != path)
                    waypoints.Add(t);

            currentWaypoint = waypoints.Count > 0 ? waypoints[0] : null;
        }

        private void GetNextWaypoint()
        {
            currentWaypointIndex++;
            currentWaypoint = currentWaypointIndex < waypoints.Count ? waypoints[currentWaypointIndex] : null;
        }

        #endregion
    }
}
