using UnityEngine;

using Zenject;

using TowerDefense.Scoring;

namespace TowerDefense.Enemies
{
    public class EnemyScore : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ScoreManager scoreManager = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int score = 10;

        #endregion

        #region BEHAVIORS

        public void GiveScore()
        {
            scoreManager.IncreaseScore(score);
        }

        #endregion
    }
}
