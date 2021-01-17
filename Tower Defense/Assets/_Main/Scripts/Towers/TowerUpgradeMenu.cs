using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace TowerDefense.Towers
{
    public class TowerUpgradeMenu : MonoBehaviour
    {
        #region FIELDS

        [Inject] private TowerSpot towerSpot = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button upgradeButton = null;
        [SerializeField] private Button destroyButton = null;
        [SerializeField] private Button cancelButton = null;

        private TowerShooter towerShooter = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            upgradeButton.onClick.AddListener(UpgradeTower);
            destroyButton.onClick.AddListener(DestroyTower);
            cancelButton.onClick.AddListener(Close);
        }

        public void Appear()
        {
            gameObject.SetActive(true);

            if (towerShooter == null)
                towerShooter = towerSpot.Tower.GetComponent<TowerShooter>();
        }

        private void UpgradeTower()
        {
            towerShooter.IncreaseLevel();
            Close();

            if (towerShooter.IsMaxLevel)
                upgradeButton.gameObject.SetActive(false);
        }

        private void DestroyTower()
        {
            Destroy(towerSpot.Tower);
            upgradeButton.gameObject.SetActive(true);
            Close();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
