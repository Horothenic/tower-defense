using UnityEngine;

using Utilities.Zenject;

namespace TowerDefense.Towers
{
    public class TowerSpot : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private GameObject gizmo = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private GameObject towerPrefab = null;

        private GameObject tower = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            gizmo.SetActive(false);
        }

        private void OnMouseEnter()
        {
            gizmo.SetActive(true);
        }

        private void OnMouseExit()
        {
            gizmo.SetActive(false);
        }

        private void OnMouseUpAsButton()
        {
            if (tower != null)
            {
                DestroyTower();
                return;
            }

            tower = ZenjectUtilities.Instantiate(towerPrefab, transform.position, towerPrefab.transform.rotation, transform);
        }

        private void DestroyTower()
        {
            Destroy(tower);
        }

        #endregion
    }
}
