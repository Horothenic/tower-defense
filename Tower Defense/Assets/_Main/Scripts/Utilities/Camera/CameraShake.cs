using UnityEngine;
using System.Collections;

namespace Utilities.Cameras
{
    public class CameraShake : MonoBehaviour
    {
        #region FIELDS

        private const float MediumDuration = 0.4f;
        private const float MediumMagnitude = 0.3f;

        #endregion

        #region BEHAVIORS

        public void MediumShake()
        {
            Shake(MediumDuration, MediumMagnitude);
        }

        public void Shake(float duration, float magnitude)
        {
            StartCoroutine(ShakeCoroutine(duration, magnitude));
        }

        private IEnumerator ShakeCoroutine(float duration, float magnitude)
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
