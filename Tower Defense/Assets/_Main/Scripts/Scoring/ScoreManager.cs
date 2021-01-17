using UnityEngine;

using Utilities.Inspector;
using Utilities.Events;

namespace TowerDefense.Scoring
{
    public class ScoreManager : MonoBehaviour
    {
        #region FIELDS

        [Header("STATES")]
        [ReadOnly] [SerializeField] private int totalScore = 0;

        #endregion

        #region EVENTS

        [Header("EVENTS")]
        public UnityIntEvent onScoreUpdated = null;

        #endregion

        #region BEHAVIORS

        public void IncreaseScore(int score)
        {
            totalScore += score;
            onScoreUpdated?.Invoke(totalScore);
        }

        #endregion
    }
}
