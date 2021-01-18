using UnityEngine;
using System.Linq;

namespace TowerDefense.Enemies
{
    [CreateAssetMenu(fileName = FileName, menuName = MenuName, order = Order)]
    public class WavesSetup : ScriptableObject
    {
        #region FIELDS

        private const string FileName = "New Waves Setup";
        private const string MenuName = "Tower Defense/Waves/" + FileName;
        private const int Order = 1;

        [Header("CONFIGURATIONS")]
        [SerializeField] private WaveConfiguration[] wavesConfigurations = null;

        #endregion

        #region BEHAVIORS

        public WaveConfiguration GetWaveConfiguration(int wave)
        {
            return wavesConfigurations.Last(waveConfiguration => waveConfiguration.StartWave <= wave);
        }

        #endregion
    }
}
