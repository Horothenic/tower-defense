using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

namespace TowerDefense.Scenes
{
    public class ResetSceneManager : MonoBehaviour
    {
        #region FIELDS

        [Inject] private BlackScreenController blackScreen = null;

        [Header("CONFIGURATION")]
        [SerializeField] private string sceneName = "Main";

        #endregion

        #region BEHAVIORS

        public void ResetScene()
        {
            blackScreen.Appear(() => SceneManager.LoadScene(sceneName));
        }

        #endregion
    }
}
