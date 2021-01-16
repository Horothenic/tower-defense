using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scenes
{
    public class ResetSceneManager : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATION")]
        [SerializeField] private string sceneName = "Main";

        #endregion

        #region BEHAVIORS

        public void ResetScene()
        {
            SceneManager.LoadScene(sceneName);
        }

        #endregion
    }
}
