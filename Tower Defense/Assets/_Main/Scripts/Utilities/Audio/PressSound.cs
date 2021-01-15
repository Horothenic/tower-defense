using UnityEngine;
using UnityEngine.EventSystems;

using Utilities.Audio;
using Zenject;

namespace Utilities.Buttons
{
    public class PressSound : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region FIELDS

        [Inject] private AudioManager audioManager = null;

        [Header("AUDIOS")]
        [SerializeField] private AudioClip sound = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private bool onPointerClick = true;
        [SerializeField] private bool onPointerDown = false;

        #endregion

        #region BEHAVIORS

        [SerializeField] private bool onPointerUp = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!onPointerClick)
                return;

            PlaySound();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!onPointerDown)
                return;

            PlaySound();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!onPointerUp)
                return;

            PlaySound();
        }

        private void PlaySound()
        {
            audioManager.PlaySound(sound);
        }

        #endregion
    }
}
