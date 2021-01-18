using UnityEngine;
using UnityEngine.UI;

using Zenject;
using DG.Tweening;

namespace TowerDefense.Enemies
{
    public class WavesUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private WavesManager wavesManager = null;

        [Header("COMPONENTS")]
        [SerializeField] private Text wavesText = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string wavesFormat = "Wave {0}";

        [Header("TWEENS")]
        [SerializeField] private float tweenDuration = 0.5f;
        [SerializeField] private float punchScale = 0.2f;

        private Tween tween = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            wavesManager.onWaveUpdated.AddListener(UpdateText);
        }

        private void OnDestroy()
        {
            tween?.Kill();
        }

        private void UpdateText(int wave)
        {
            tween = transform.DOPunchScale(Vector3.one * punchScale, tweenDuration);
            wavesText.text = string.Format(wavesFormat, wave);
        }

        #endregion
    }
}
