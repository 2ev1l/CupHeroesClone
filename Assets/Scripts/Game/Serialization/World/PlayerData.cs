using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class PlayerData: ICloneable<PlayerData>
    {
        #region fields & properties
        public Wallet Wallet => wallet;
        [SerializeField] private Wallet wallet = new(0);
        public EntityStats Stats => stats;
        [SerializeField] private EntityStats stats = new();
        #endregion fields & properties

        #region methods
        public PlayerData Clone()
        {
            return new()
            {
                wallet = wallet.Clone(),
                stats = stats.Clone()
            };
        }
        #endregion methods
    }
}