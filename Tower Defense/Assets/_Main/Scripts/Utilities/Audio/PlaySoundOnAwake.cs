using UnityEngine;

using Zenject;

using Utilities.Audio;

namespace Utilities.Audio
{
    public class PlaySoundOnAwake : MonoBehaviour
    {
        #region FIELDS

        [Inject] private AudioManager audioManager = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private AudioClip[] audios = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            PlayRandomSound();
        }

        private void PlayRandomSound()
        {
            if (audios.Length == 0)
                return;

            audioManager.PlaySound(audios[Random.Range(default(int), audios.Length)]);
        }

        #endregion
    }
}
