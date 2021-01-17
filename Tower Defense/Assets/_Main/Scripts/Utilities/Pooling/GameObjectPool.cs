using System.Collections.Generic;
using UnityEngine;

using Utilities.Zenject;

namespace Utilities.Pooling
{
    public class GameObjectPool<T> : MonoBehaviour where T : UnityEngine.Object
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform parent = null;
        [SerializeField] private bool useZenject = false;

        private Dictionary<T, List<T>> poolDictionary = new Dictionary<T, List<T>>();

        #endregion

        #region BEHAVIORS

        public T Spawn(T gameObject)
        {
            return Spawn(gameObject, Vector3.zero, Quaternion.identity);
        }

        public T Spawn(T gameObject, Vector3 position)
        {
            return Spawn(gameObject, position, Quaternion.identity);
        }

        public T Spawn(T gameObject, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(gameObject))
                poolDictionary.Add(gameObject, new List<T>());

            T objectToSpawn = poolDictionary[gameObject].Find(foundObject => !GetGameObject(foundObject).activeSelf);

            if (objectToSpawn == null)
            {
                if (useZenject)
                    objectToSpawn = ZenjectUtilities.Instantiate(gameObject, position, rotation, parent);
                else
                    objectToSpawn = Instantiate(gameObject, position, rotation, parent);

                poolDictionary[gameObject].Add(objectToSpawn);
            }
            else
            {
                var gameObjectToSpawn = GetGameObject(objectToSpawn);
                gameObjectToSpawn.transform.position = position;
                gameObjectToSpawn.transform.rotation = rotation;
                gameObjectToSpawn.SetActive(true);
            }

            return objectToSpawn;
        }

        private GameObject GetGameObject(T obj)
        {
            return (obj as MonoBehaviour).gameObject;
        }

        #endregion
    }
}
