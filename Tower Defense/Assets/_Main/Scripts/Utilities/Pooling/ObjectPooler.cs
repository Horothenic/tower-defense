using System.Collections.Generic;
using UnityEngine;

using Utilities.Zenject;

namespace Utilities.Pooling
{
    public class ObjectPooler : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform poolParent = null;
        [SerializeField] private bool useZenject = false;

        private Dictionary<GameObject, List<GameObject>> poolDictionary = new Dictionary<GameObject, List<GameObject>>();

        #endregion

        #region BEHAVIORS

        public GameObject Spawn(GameObject gameObject)
        {
            return Spawn(gameObject, Vector3.zero, Quaternion.identity);
        }

        public GameObject Spawn(GameObject gameObject, Vector3 position)
        {
            return Spawn(gameObject, position, Quaternion.identity);
        }

        public GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(gameObject))
                poolDictionary.Add(gameObject, new List<GameObject>());

            GameObject objectToSpawn = poolDictionary[gameObject].Find(foundObject => !foundObject.activeSelf);
            if (objectToSpawn == null)
            {
                if (useZenject)
                    objectToSpawn = ZenjectUtilities.Instantiate(gameObject, position, rotation, poolParent);
                else
                    objectToSpawn = Instantiate(gameObject, position, rotation, poolParent);

                poolDictionary[gameObject].Add(objectToSpawn);
            }
            else
            {
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                objectToSpawn.SetActive(true);
            }

            return objectToSpawn;
        }

        #endregion
    }
}
