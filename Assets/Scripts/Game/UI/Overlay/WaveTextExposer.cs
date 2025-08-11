using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class WaveTextExposer : MonoBehaviour
    {
        #region fields & properties
        private static string WaveTextLanguage => LanguageInfo.GetTextByType(TextType.Game, 0);
        [SerializeField] private TextMeshProUGUI waveText;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            GameData.Data.WaveData.OnWaveIncreased += UpdateText;
            UpdateText(GameData.Data.WaveData.CurrentWave);
        }
        private void OnDisable()
        {
            GameData.Data.WaveData.OnWaveIncreased -= UpdateText;
        }
        private void UpdateText(int currentWave)
        {
            int totalWaves = DB.Instance.Waves.Data.Count;
            waveText.text = $"{WaveTextLanguage} {currentWave + 1} / {totalWaves}";
        }
        #endregion methods
    }
}