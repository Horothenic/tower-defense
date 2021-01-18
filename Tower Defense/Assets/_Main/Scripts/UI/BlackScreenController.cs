using UnityEngine;

using DG.Tweening;

namespace TowerDefense.Scenes
{
    public class BlackScreenController : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private CanvasGroup canvasGroup = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private bool disappearOnStart = true;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            if (disappearOnStart)
                Disappear();
        }

        public void Appear(TweenCallback onComplete = null)
        {
            canvasGroup.DOFade(1, duration).From(0).OnComplete(onComplete);
        }

        public void Disappear(TweenCallback onComplete = null)
        {
            canvasGroup.DOFade(0, duration).From(1).OnComplete(onComplete);
        }

        #endregion
    }
}
