using UnityEngine;

namespace TowerDefense.Towers
{
    public class MegaTowerSpot : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private GameObject gizmo = null;
        [SerializeField] private MegaTowerShooter megaTowerShooter = null;

        #endregion

        #region BEHAVIORS

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
            megaTowerShooter.Shoot();
        }

        #endregion
    }
}
