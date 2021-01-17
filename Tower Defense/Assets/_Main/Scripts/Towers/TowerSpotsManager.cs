using UnityEngine;

namespace TowerDefense.Towers
{
    public class TowerSpotsManager : MonoBehaviour
    {
        #region EVENTS

        [HideInInspector] public UnityTowerSpotEvent onTowerSpotSelected = null;

        #endregion

        #region BEHAVIORS

        public void OnTowerSpotSelected(TowerSpot towerSpot)
        {
            onTowerSpotSelected?.Invoke(towerSpot);
        }

        #endregion
    }
}
