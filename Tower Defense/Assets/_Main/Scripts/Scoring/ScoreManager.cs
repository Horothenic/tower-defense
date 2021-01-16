using UnityEngine;

using Utilities.Inspector;

namespace TowerDefense.Scoring
{
    public class ScoreManager : MonoBehaviour
    {
        #region FIELDS

        [Header("STATES")]
        [ReadOnly] [SerializeField] private int totalScore = 0;

        #endregion

        #region BEHAVIORS

        public void IncreaseScore(int score)
        {
            totalScore += score;
        }

        #endregion
    }
}
