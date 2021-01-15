using UnityEngine;

using Zenject;

namespace Utilities.Zenject
{
    public static class ZenjectUtilities
    {
        #region FIELDS

        private const string ZenjectSceneContextObjectName = "Zenject";

        private static SceneContext sceneContext = null;

        #endregion

        #region PROPERTIES

        public static SceneContext SceneContext { get => sceneContext == null ? sceneContext = GetSceneContext() : sceneContext; }
        public static DiContainer Container { get => SceneContext.Container; }

        #endregion

        #region BEHAVIORS

        private static SceneContext GetSceneContext()
        {
            return GameObject.Find(ZenjectSceneContextObjectName).GetComponentInChildren<SceneContext>();
        }

        public static T Instantiate<T>(T prefab, Context context = null) where T : UnityEngine.Object
        {
            return Instantiate<T>(prefab, Vector3.zero, Quaternion.identity, null, context);
        }

        public static T Instantiate<T>(T prefab, Transform parent, Context context = null) where T : UnityEngine.Object
        {
            return Instantiate<T>(prefab, Vector3.zero, Quaternion.identity, parent, context);
        }

        public static T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent, Context context = null) where T : UnityEngine.Object
        {
            var newObject = context != null ? context.Container.InstantiatePrefab(prefab, position, rotation, parent) : Container.InstantiatePrefab(prefab, position, rotation, parent);
            var prefabTransform = prefab is GameObject ? (prefab as GameObject).transform : (prefab as MonoBehaviour).transform;
            newObject.transform.localScale = prefabTransform.localScale;
            if (prefabTransform is RectTransform)
                CheckRectTransform(newObject.transform as RectTransform, prefabTransform as RectTransform);

            return (prefab is GameObject) ? newObject as T : newObject.GetComponent<T>();
        }

        private static void CheckRectTransform(RectTransform newObject, RectTransform prefabTransform)
        {
            newObject.anchorMin = prefabTransform.anchorMin;
            newObject.anchorMax = prefabTransform.anchorMax;
            newObject.anchoredPosition = prefabTransform.anchoredPosition;
            newObject.sizeDelta = prefabTransform.sizeDelta;
        }

        #endregion
    }
}
