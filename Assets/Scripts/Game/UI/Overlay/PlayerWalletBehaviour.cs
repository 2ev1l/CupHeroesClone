using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class PlayerWalletBehaviour : WalletBehaviour
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        protected override void Initialize()
        {
            Wallet = GameData.Data.PlayerData.Wallet;
        }
        #endregion methods
    }
}