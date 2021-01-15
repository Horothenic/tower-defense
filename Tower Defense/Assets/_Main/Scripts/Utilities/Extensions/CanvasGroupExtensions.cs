using UnityEngine;

using DG.Tweening;

namespace Utilities.Extensions
{
    public static class CanvasGroupExtensions
    {
        #region FIELDS

        private const float FadeDuration = 0.3f;
        private const float NoFade = 1f;
        private const float ClearFade = 0f;

        #endregion

        #region BEHAVIORS

        public static Tween Show(this CanvasGroup canvasGroup, float duration = FadeDuration)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.blocksRaycasts = true;
            return canvasGroup.DOFade(NoFade, duration).From(ClearFade, true);
        }

        public static Tween Hide(this CanvasGroup canvasGroup, float duration = FadeDuration, bool turnOffObject = true)
        {
            canvasGroup.blocksRaycasts = false;
            return canvasGroup.DOFade(ClearFade, duration).OnComplete(() => canvasGroup.gameObject.SetActive(!turnOffObject));
        }

        public static void Reset(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = NoFade;
            canvasGroup.blocksRaycasts = true;
        }

        #endregion
    }
}
