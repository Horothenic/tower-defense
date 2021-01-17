using UnityEngine;
using UnityEngine.UI;

using Zenject;
using DG.Tweening;

namespace TowerDefense.Scoring
{
    public class ScoreUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ScoreManager scoreManager = null;

        [Header("COMPONENTS")]
        [SerializeField] private Text scoreText = null;

        [Header("TWEENS")]
        [SerializeField] private float tweenDuration = 0.5f;
        [SerializeField] private float punchScale = 0.2f;

        private int currentScore = 0;
        private Sequence tweenSequence = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            scoreManager.onScoreUpdated.AddListener(UpdateText);
        }

        private void OnDestroy()
        {
            tweenSequence?.Kill();
        }

        private void UpdateText(int totalScore)
        {
            tweenSequence?.Kill(true);

            tweenSequence = DOTween.Sequence();
            tweenSequence.Insert(0, transform.DOPunchScale(Vector3.one * punchScale, tweenDuration));
            tweenSequence.Insert(0, IncreaseNumbers(currentScore, totalScore));

            currentScore = totalScore;
        }

        private Tween IncreaseNumbers(int from, int to)
        {
            return DOTween.To(() => from, x => scoreText.text = x.ToString(), to, tweenDuration);
        }

        #endregion
    }
}
