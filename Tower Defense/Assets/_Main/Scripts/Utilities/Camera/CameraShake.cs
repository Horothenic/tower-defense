using UnityEngine;
using System.Collections;

namespace Utilities.Cameras
{
    public class CameraShake : MonoBehaviour
    {
        #region BEHAVIORS

        public void RunShake(float duration, float magnitude)
        {
            StartCoroutine(Shake(duration, magnitude));
        }

        private IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 orignalPosition = transform.position;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                float x = orignalPosition.x + UnityEngine.Random.Range(-1f, 1f) * magnitude;
                float y = orignalPosition.y + UnityEngine.Random.Range(-1f, 1f) * magnitude;

                transform.position = new Vector3(x, y, orignalPosition.z);
                elapsed += UnityEngine.Time.deltaTime;
                yield return null;
            }

            transform.position = orignalPosition;
        }

        #endregion
    }
}
