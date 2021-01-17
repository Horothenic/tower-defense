using UnityEngine;

using Zenject;

namespace TowerDefense.Towers
{
    public class TowerSpot : MonoBehaviour
    {
        #region FIELDS

        [Inject] private TowerSpotsManager towerSpotsManager = null;

        [Header("COMPONENTS")]
        [SerializeField] private GameObject gizmo = null;
        [SerializeField] private TowerCreationMenu creationMenu = null;
        [SerializeField] private TowerUpgradeMenu upgradeMenu = null;

        #endregion

        #region PROPERTIES

        public GameObject Tower { get; private set; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            towerSpotsManager.onTowerSpotSelected.AddListener(DeactivateMenus);
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
            towerSpotsManager.OnTowerSpotSelected(this);

            if (Tower == null)
                creationMenu.Appear();
            else
                upgradeMenu.Appear();
        }

        public void SetTower(GameObject newTower)
        {
            Tower = newTower;
        }

        private void DeactivateMenus(TowerSpot towerSpot)
        {
            if (towerSpot == this)
                return;

            creationMenu.Close();
            upgradeMenu.Close();
        }

        #endregion
    }
}
