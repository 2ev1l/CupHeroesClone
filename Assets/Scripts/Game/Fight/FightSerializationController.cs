using EditorCustom.Attributes;
using Game.Serialization;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Serialization;

namespace Game.Fight
{
    public class FightSerializationController : MonoBehaviour
    {
        #region fields & properties
        private PlayerData lastPlayerData = null;
        private int waveSaved = -1;
        private GameData gameData = null;
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void SaveParameters()
        {
            gameData = GameData.Data;
            lastPlayerData = gameData.PlayerData.Clone();
            waveSaved = gameData.WaveData.CurrentWave;
        }
        private void OnApplicationQuit()
        {
            if (TryLoadParameters())
                SavingController.Instance.SaveGameData();
        }
        private void OnDestroy()
        {
            TryLoadParameters();
        }
        private bool TryLoadParameters()
        {
            if (waveSaved < 0 || lastPlayerData == null) return false;
            LoadParameters();
            return true;
        }
        public void LoadParameters()
        {
            gameData.SetPlayerData(lastPlayerData);
        }
        #endregion methods
    }
}