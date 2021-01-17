using UnityEngine;
using UnityEngine.UI;

using Zenject;

using Utilities.Zenject;

namespace TowerDefense.Towers
{
    public class TowerCreationMenu : MonoBehaviour
    {
        #region FIELDS

        [Inject] private TowerSpot towerSpot = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button createButton = null;
        [SerializeField] private Button[] cancelButtons = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private GameObject towerPrefab = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            createButton.onClick.AddListener(CreateTower);

            foreach (var button in cancelButtons)
                button.onClick.AddListener(Close);
        }

        public void Appear()
        {
            gameObject.SetActive(true);
        }

        private void CreateTower()
        {
            var pivot = transform.parent;
            var tower = ZenjectUtilities.Instantiate(towerPrefab, pivot.position, towerPrefab.transform.rotation, pivot);
            towerSpot.SetTower(tower);
            Close();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
