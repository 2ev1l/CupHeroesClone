using Game.DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class WaveData
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - currentWave;
        /// </summary>
        public UnityAction<int> OnWaveIncreased;

        public WaveInfo CurrentWaveInfo => DB.Instance.Waves.Data[currentWave].Data;
        public int CurrentWave => currentWave;
        [SerializeField] private int currentWave = 0;
        #endregion fields & properties

        #region methods
        public bool TryIncreaseWave()
        {
            if (currentWave >= DB.Instance.Waves.Data.Count) return false;
            IncreaseWave();
            return true;
        }
        private void IncreaseWave()
        {
            currentWave++;
        }
        #endregion methods
    }
}