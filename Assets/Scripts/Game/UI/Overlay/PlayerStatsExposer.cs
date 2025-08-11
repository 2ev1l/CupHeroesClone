using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class PlayerStatsExposer : EntityStatsExposer
    {
        #region fields & properties
        [SerializeField] private TextMeshProUGUI walletText;
        #endregion fields & properties

        #region methods
        public void SetWalletText(int value) => walletText.text = value.ToString();
        #endregion methods
    }
}