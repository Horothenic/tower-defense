using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace TowerDefense.UI
{
    public class DamageDealtUI : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private RectTransform textRectTransform = null;
        [SerializeField] private Text text = null;

        [Header("TWEEN")]
        [SerializeField] private float startHeight = -50;
        [SerializeField] private float endHeight = 50;
        [SerializeField] private float fadeDuration = 0.2f;
        [SerializeField] private float movementDuration = 0.8f;

        private Sequence tweenSequence = null;

        #endregion

        #region BEHAVIORS

        public void LoadDamageDealt(float damage)
        {
            text.text = ((int)damage).ToString();
            ShowTween();
        }

        private void ShowTween()
        {
            tweenSequence = DOTween.Sequence();

            var time = 0f;
            tweenSequence.Insert(time, canvasGroup.DOFade(1, fadeDuration).From(0));
            tweenSequence.Insert(time, textRectTransform.DOAnchorPosY(endHeight, movementDuration).From(Vector2.up * startHeight));

            time += movementDuration - fadeDuration;

            tweenSequence.Insert(time, canvasGroup.DOFade(0, fadeDuration).From(1));
            tweenSequence.AppendCallback(Destroy);

            gameObject.SetActive(true);
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
