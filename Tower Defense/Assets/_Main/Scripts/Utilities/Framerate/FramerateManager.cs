using UnityEngine;

namespace Framerate
{
    public class FramerateManager : MonoBehaviour
    {
        #region FIELDS

        private const int MinimumFramerate = 30;
        private const int MaximumFramerate = 60;

        [Header("CONFIGURATIONS")]
        [SerializeField] [Range(MinimumFramerate, MaximumFramerate)] private int desiredFramerate = 60;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            Application.targetFrameRate = desiredFramerate;
        }

        #endregion
    }
}
