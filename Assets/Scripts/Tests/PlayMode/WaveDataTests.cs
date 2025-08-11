using System.Collections;
using System.Collections.Generic;
using Game.DataBase;
using Game.Serialization.World;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Universal.Events;

namespace Tests.PlayMode
{
    public class WaveDataTests
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        [Test]
        public void IncreaseWavePositiveTest()
        {
            AssetLoader.InitInstances();
            GameData.SetData(new());
            Assert.IsTrue(GameData.Data.WaveData.TryIncreaseWave());
            Assert.AreEqual(1, GameData.Data.WaveData.CurrentWave);
        }
        [Test]
        public void IncreaseWaveNegativeTest()
        {
            AssetLoader.InitInstances();
            GameData.SetData(new());
            int waves = DB.Instance.Waves.Data.Count;
            int safety = 1000000;
            int i = 0;
            while (GameData.Data.WaveData.CurrentWave < waves)
            {
                if (!GameData.Data.WaveData.TryIncreaseWave() || i > safety)
                {
                    Assert.Fail($"safety? {i > safety}");
                    return;
                }
                i++;
            }
            Assert.IsFalse(GameData.Data.WaveData.TryIncreaseWave());
        }
        #endregion methods
    }
}