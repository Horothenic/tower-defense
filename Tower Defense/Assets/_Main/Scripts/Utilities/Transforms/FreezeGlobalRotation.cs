using UnityEngine;

namespace Utilities.Transforms
{
    public class FreezeGlobalRotation : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private Vector3 forcedRotation = default(Vector3);

        #endregion

        #region BEHAVIORS

        private void Update()
        {
            transform.rotation = Quaternion.Euler(forcedRotation);
        }

        #endregion
    }
}
