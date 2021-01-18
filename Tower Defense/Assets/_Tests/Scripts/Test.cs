using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace Tests
{
    public class Test
    {
        #region FIELDS

        private const string SceneFilter = "t:SceneAsset ";
        private const string TestScenesFolderPath = "Assets/_Tests/Scenes";
        private const float FastTimescale = 5f;
        private const float NormalTimescale = 1f;

        #endregion

        #region BEHAVIORS

        protected void LoadScene(string sceneName)
        {
            var foundScenes = AssetDatabase.FindAssets(SceneFilter + sceneName, new string[] { TestScenesFolderPath });
            var scenePath = AssetDatabase.GUIDToAssetPath(foundScenes.First());
            EditorSceneManager.LoadSceneAsyncInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Additive));
        }

        protected void UnloadScene(string sceneName)
        {
            var scene = EditorSceneManager.GetSceneByName(sceneName);
            EditorSceneManager.UnloadSceneAsync(scene);
        }

        protected void SetFastTimescale()
        {
            Time.timeScale = FastTimescale;
        }

        protected void ResetTimescale()
        {
            Time.timeScale = NormalTimescale;
        }

        #endregion
    }
}
