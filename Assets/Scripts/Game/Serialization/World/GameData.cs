using EditorCustom.Attributes;
using Game.DataBase;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class GameData
    {
        #region fields & properties
        public static readonly string SaveName = "save";
        public static readonly string SaveExtension = ".data";

        public static GameData Data => data;
        private static GameData data;

        public PlayerData PlayerData => playerData;
        [SerializeField] private PlayerData playerData = new();

        public WaveData WaveData => waveData;
        [SerializeField] private WaveData waveData = new();
        #endregion fields & properties

        #region methods
        public static void SetData(GameData value) => data = value;
        /// <summary>
        /// This method will update nothing in scene. Using as backup
        /// </summary>
        /// <param name="value"></param>
        public void SetPlayerData(PlayerData value)
        {
            playerData = value;
        }
        #endregion methods
    }
}